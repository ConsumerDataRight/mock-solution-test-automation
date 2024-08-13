using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Jose;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    public static class Helpers
    {
        public static class Web
        {
            public static HttpClient CreateHttpClient(string? certFilename = null, string? certPassword = null, bool allowAutoRedirect = true, IEnumerable<string>? cookies = null, HttpRequestMessage? request = null, CookieContainer? cookieContainer = null)
            {
                var clientHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = allowAutoRedirect,
                };
                clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true; //sonarqube will raise this as a vulnerability as it is not away this is a test library only

                // Set cookie container
                if (cookieContainer != null)
                {
                    if (cookies != null)
                    {
                        throw new ArgumentOutOfRangeException(nameof(cookies),"Cookies and CookieContainer parameters cannot be provided at the same time.").Log();
                    }
                    clientHandler.UseDefaultCredentials = true; //used with the Cookie Container
                    clientHandler.UseCookies = true;
                    clientHandler.CookieContainer = cookieContainer;
                }

                // Set cookies
                if (cookies != null)
                {
                    if (request == null)
                    {
                        throw new ArgumentNullException(nameof(request),"Request parameter cannot be null when Cookies parameter has been provided.").Log();
                    }

                    clientHandler.UseCookies = false;
                    request.Headers.Add("Cookie", cookies);
                }

                // Attach client certificate
                if (certFilename != null)
                {
                    if (certPassword == null)
                    {
                        throw new ArgumentNullException(nameof(certPassword),"Certificate password parameter cannot be null when Certificate filename parameter has been provided.").Log();
                    }

                    clientHandler.ClientCertificates.Add(new X509Certificate2(
                        certFilename,
                        certPassword,
                        X509KeyStorageFlags.Exportable
                    ));
                }

                return new HttpClient(new LoggingHandler(clientHandler));
            }
        }

        public static class Jwt
        {
            public static string CreateJWT(string certificateFilename, string certificatePassword, Dictionary<string, object> subject)
            {
                var payload = JsonConvert.SerializeObject(subject);

                return CreateJWT(certificateFilename, certificatePassword, payload);
            }

            public static string CreateJWT(string certificateFilename, string certificatePassword, string payload)
            {
                var cert = new X509Certificate2(certificateFilename, certificatePassword);

                var securityKey = new X509SecurityKey(cert);

                var jwtHeader = new Dictionary<string, object>()
            {
                { JwtHeaderParameterNames.Alg, SecurityAlgorithms.RsaSsaPssSha256 },
                { JwtHeaderParameterNames.Typ, "JWT" },
                { JwtHeaderParameterNames.Kid, securityKey.KeyId},
            };

                var jwt = JWT.Encode(payload, cert.GetRSAPrivateKey(), JwsAlgorithm.PS256, jwtHeader);

                return jwt;
            }
        }

        public static class Jwk
        {
            /// <summary>
            /// Build JWKS from certificate
            /// </summary>
            public static Jwks BuildJWKS(string certificateFilename, string certificatePassword)
            {
                var cert = new X509Certificate2(certificateFilename, certificatePassword);

                //Get credentials from certificate
                var securityKey = new X509SecurityKey(cert);
                var signingCredentials = new X509SigningCredentials(cert, SecurityAlgorithms.RsaSsaPssSha256);
                var encryptingCredentials = new X509EncryptingCredentials(cert, SecurityAlgorithms.RsaOaepKeyWrap, SecurityAlgorithms.RsaOAEP);

                var rsaParams = signingCredentials.Certificate.GetRSAPublicKey()?.ExportParameters(false) ?? throw new Exception("Error getting RSA params").Log();
                var e = Base64UrlEncoder.Encode(rsaParams.Exponent);
                var n = Base64UrlEncoder.Encode(rsaParams.Modulus);

                var jwkSign = new Models.Jwk()
                {
                    Alg = signingCredentials.Algorithm,
                    Kid = signingCredentials.Kid,
                    //  kid = signingCredentials.Key.KeyId,
                    Kty = securityKey.PublicKey.KeyExchangeAlgorithm,
                    N = n,
                    E = e,
                    Use = "sig"
                };

                var jwkEnc = new Models.Jwk()
                {
                    Alg = encryptingCredentials.Enc,
                    Kid = encryptingCredentials.Key.KeyId,
                    Kty = securityKey.PublicKey.KeyExchangeAlgorithm,
                    N = n,
                    E = e,
                    Use = "enc"
                };

                return new Jwks()
                {
                    Keys = [jwkSign, jwkEnc]
                };
            }
        }

        public static class AuthServer
        {
            // When running standalone CdrAuthServer (ie no MtlsGateway) we need to attach the X-TlsClientCertThumbprint required by ValidateMTLSAttribute
            static public void AttachHeadersForStandAlone(string url, HttpHeaders headers, string dhMtlsGatewayUrl, string xtlsClientCertThumbprint, bool? isStandalone = false)
            {
                if (isStandalone.HasValue && isStandalone.Value)
                {
                    if (dhMtlsGatewayUrl.IsNullOrWhiteSpace())
                    {
                        throw new ArgumentNullException(nameof(dhMtlsGatewayUrl)).Log();
                    }

                    if (url.StartsWith(dhMtlsGatewayUrl))
                    {
                        if (xtlsClientCertThumbprint.IsNullOrWhiteSpace())
                        {
                            throw new ArgumentNullException(nameof(xtlsClientCertThumbprint)).Log();
                        }

                        headers.Add("X-TlsClientCertThumbprint", xtlsClientCertThumbprint);
                    }
                }
            }

            /// <summary>
            /// Clear data from the Dataholder's AuthServer database
            /// </summary>
            /// <param name="options"></param>
            /// <param name="onlyPersistedGrants">Only clear the persisted grants table</param>
            public static void PurgeAuthServerForDataholder(TestAutomationOptions options, bool onlyPersistedGrants = false)
            {
                Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(PurgeAuthServerForDataholder), nameof(Helpers.AuthServer));

                using var connection = new SqlConnection(options.AUTHSERVER_CONNECTIONSTRING);

                void Purge(string table)
                {
                    // Delete all rows
                    using var deleteCommand = new SqlCommand($"delete from {table}", connection);
                    deleteCommand.ExecuteNonQuery();

                    // Check all rows deleted
                    using var selectCommand = new SqlCommand($"select count(*) from {table}", connection);
                    var count = selectCommand.ExecuteScalarInt32();
                    if (count != 0)
                    {
                        throw new InvalidOperationException($"Table {table} was not purged").Log();
                    }
                }

                try
                {
                    connection.Open();

                    if (!onlyPersistedGrants)
                    {
                        Purge("ClientClaims");
                        Purge("Clients");
                    }

                    Purge("Grants");
                }
                catch (Exception ex)
                {
                    ex.LogAndThrow();
                }
            }

            /// <summary>
            /// The seed data for the Register is using the loopback uri for redirecturi.
            /// Since the integration tests stands up it's own data recipient consent/callback endpoint we need to 
            /// patch the redirect uri to match our callback.
            /// </summary>
            public static void PatchRedirectUriForRegister(TestAutomationOptions options, string softwareProductId = TestAutomation.Constants.SoftwareProducts.SoftwareProductId, string redirectURI = "")
            {
                Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(PatchRedirectUriForRegister), nameof(Helpers.AuthServer));

                if (redirectURI.IsNullOrWhiteSpace())
                {
                    redirectURI = options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
                }

                try
                {
                    using var connection = new SqlConnection(options.REGISTER_CONNECTIONSTRING);
                    connection.Open();

                    using var updateCommand = new SqlCommand("update softwareproduct set redirecturis = @uri where lower(softwareproductid) = @id", connection);
                    updateCommand.Parameters.AddWithValue("@uri", redirectURI);
                    updateCommand.Parameters.AddWithValue("@id", softwareProductId.ToLower());
                    updateCommand.ExecuteNonQuery();

                    using var selectCommand = new SqlCommand($"select redirecturis from softwareproduct where lower(softwareproductid) = @id", connection);
                    selectCommand.Parameters.AddWithValue("@id", softwareProductId.ToLower());
                    if (selectCommand.ExecuteScalarString() != redirectURI)
                    {
                        throw new InvalidOperationException($"softwareproduct.redirecturis is not '{redirectURI}'").Log();
                    }
                }
                catch (Exception ex)
                {
                    ex.LogAndThrow();
                }

            }


            /// <summary>
            /// The seed data for the Register is using the loopback uri for jwksuri.
            /// Since the integration tests stands up it's own data recipient jwks endpoint we need to 
            /// patch the jwks uri to match our endpoint.
            /// </summary>
            public static void PatchJwksUriForRegister(TestAutomationOptions options, string softwareProductId = TestAutomation.Constants.SoftwareProducts.SoftwareProductId, string jwksURI = "")
            {
                Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(PatchJwksUriForRegister), nameof(Helpers.AuthServer));

                if (jwksURI.IsNullOrWhiteSpace())
                {
                    jwksURI = options.SOFTWAREPRODUCT_JWKS_URI_FOR_INTEGRATION_TESTS;
                }

                try 
                { 
                    using var connection = new SqlConnection(options.REGISTER_CONNECTIONSTRING);
                    connection.Open();

                    using var updateCommand = new SqlCommand("update softwareproduct set jwksuri = @uri where lower(softwareproductid) = @id", connection);
                    updateCommand.Parameters.AddWithValue("@uri", jwksURI);
                    updateCommand.Parameters.AddWithValue("@id", softwareProductId.ToLower());
                    updateCommand.ExecuteNonQuery();

                    using var selectCommand = new SqlCommand($"select jwksuri from softwareproduct where lower(softwareproductid) = @id", connection);
                    selectCommand.Parameters.AddWithValue("@id", softwareProductId.ToLower());
                    if (selectCommand.ExecuteScalarString() != jwksURI)
                    {
                        throw new InvalidOperationException($"softwareproduct.jwksuri is not '{jwksURI}'").Log();
                    }
                }
                catch (Exception ex)
                {
                    ex.LogAndThrow();
                }
            }

            public static void UpdateAuthServerClientClaim(TestAutomationOptions options, string clientId, string claimType, string value)
            {
                Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(UpdateAuthServerClientClaim), nameof(Helpers.AuthServer));

                try
                {
                    using var connection = new SqlConnection(options.AUTHSERVER_CONNECTIONSTRING);
                    connection.Open();
                    using var updateCommand = new SqlCommand(
                        $"Update ClientClaims Set Value = '{value}' WHERE ClientId = '{clientId}' AND Type = '{claimType}'",
                        connection);
                    int updatedRowCount = updateCommand.ExecuteNonQuery();
                    if (updatedRowCount == 0)
                    {
                        throw new InvalidOperationException($"Update AuthServer ClientClaim for client {clientId} failed.").Log();
                    }
                }
                catch (Exception ex)
                {
                    ex.LogAndThrow();
                }
            }
        }
    }

}