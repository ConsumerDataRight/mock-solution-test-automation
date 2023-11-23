using System.IdentityModel.Tokens.Jwt;
using System.Net;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public class DataHolderRegisterService : IDataHolderRegisterService
    {
        private readonly TestAutomationOptions _options;
        private readonly IRegisterSsaService _registerSSAService;
        private readonly IApiServiceDirector _apiServiceDirector;

        private readonly string _latestSSAVersion = "3";

        public DataHolderRegisterService(IOptions<TestAutomationOptions> options, IRegisterSsaService registerSSAService, IApiServiceDirector apiServiceDirector)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _registerSSAService = registerSSAService ?? throw new ArgumentNullException(nameof(registerSSAService));
            _apiServiceDirector = apiServiceDirector ?? throw new ArgumentNullException(nameof(apiServiceDirector));
        }
        /// <summary>
        /// Create registration request JWT for SSA
        /// </summary>
        public string CreateRegistrationRequest(
            string ssa,
            string tokenEndpointAuthSigningAlg = "PS256",
            string[]? redirectUris = null,
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword,
            string applicationType = "web",
            string requestObjectSigningAlg = "PS256",
            string responseType = "code id_token",
            string[]? grantTypes = null,
            string? authorizationSignedResponseAlg = null,
            string? authorizationEncryptedResponseAlg = null,
            string? authorizationEncryptedResponseEnc = null,
            string? idTokenSignedResponseAlg = "PS256",
            string? idTokenEncryptedResponseAlg = "RSA-OAEP",
            string? idTokenEncryptedResponseEnc = "A256GCM"
            )
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(CreateRegistrationRequest), nameof(DataHolderRegisterService));

            string[] responseTypes = responseType.Contains(",") ? responseType.Split(",") : new string[] { responseType };

            grantTypes = grantTypes ?? new string[] { "client_credentials", "authorization_code", "refresh_token" };

            JwtSecurityToken decodedSSA;
            try
            {
                decodedSSA = new JwtSecurityTokenHandler().ReadJwtToken(ssa);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error attempting to decode SSA.\r\nSSA={ssa}.\r\nException={ex.Message}").Log();
            }

            var softwareId = decodedSSA.Claims.First(claim => claim.Type == "software_id").Value;

            var iat = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
            var exp = iat + 300; // expire 5 mins from now

            var subject = new Dictionary<string, object>
                {
                    { "iss", softwareId },
                    { "iat", iat },
                    { "exp", exp },
                    { "jti", Guid.NewGuid().ToString() },
                    { "aud", _options.REGISTRATION_AUDIENCE_URI },

                    // Get redirect_uris from SSA
                    { "redirect_uris",
                        redirectUris ??
                        decodedSSA.Claims.Where(claim => claim.Type == "redirect_uris").Select(claim => claim.Value).ToArray() },

                    { "token_endpoint_auth_signing_alg", tokenEndpointAuthSigningAlg },
                    { "token_endpoint_auth_method", "private_key_jwt" },
                    { "grant_types", grantTypes },
                    { "response_types", responseTypes },

                    //{ "id_token_signed_response_alg", "PS256" }, //TODO: Optional?
                    //{ "id_token_encrypted_response_alg", "RSA-OAEP" },  //TODO: Optional?
                    //{ "id_token_encrypted_response_enc", "A256GCM" },  //TODO: Optional?
                    //{ "application_type", applicationType }, // spec says optional
                    { "software_statement", ssa },

                    { "client_id",softwareId },
                };

            // Optional fields.
            if (!string.IsNullOrEmpty(applicationType))
            {
                subject.Add("application_type", applicationType);
            }

            if (!string.IsNullOrEmpty(requestObjectSigningAlg))
            {
                subject.Add("request_object_signing_alg", requestObjectSigningAlg);
            }

            if (authorizationSignedResponseAlg != null)
            {
                if (authorizationSignedResponseAlg == Constants.Null)
                {
                    subject.Add("authorization_signed_response_alg", null);
                }
                else
                {
                    subject.Add("authorization_signed_response_alg", authorizationSignedResponseAlg);
                }
            }

            if (authorizationEncryptedResponseAlg != null)
            {
                subject.Add("authorization_encrypted_response_alg", authorizationEncryptedResponseAlg);
            }
            if (authorizationEncryptedResponseEnc != null)
            {
                subject.Add("authorization_encrypted_response_enc", authorizationEncryptedResponseEnc);
            }

            if (idTokenSignedResponseAlg != null)
            {
                subject.Add("id_token_signed_response_alg", idTokenSignedResponseAlg);
            }

            if (idTokenEncryptedResponseAlg != null)
            {
                subject.Add("id_token_encrypted_response_alg", idTokenEncryptedResponseAlg);
            }
            if (idTokenEncryptedResponseEnc != null)
            {
                subject.Add("id_token_encrypted_response_enc", idTokenEncryptedResponseEnc);
            }

            var jwt = Helpers.Jwt.CreateJWT(
               jwtCertificateFilename,
               jwtCertificatePassword,
               subject);

            return jwt;
        }

        /// <summary>
        /// Register software product using registration request
        /// </summary>
        public async Task<HttpResponseMessage> RegisterSoftwareProduct(string registrationRequest)
        {
            Log.Information("Calling {FUNCTION} in {ClassName} with Params: {P1}={V1}.", nameof(RegisterSoftwareProduct), nameof(DataHolderRegisterService), nameof(registrationRequest), registrationRequest);

            var url = $"{_options.DH_MTLS_GATEWAY_URL}/connect/register";

            var accessToken = new PrivateKeyJwtService()
            {
                CertificateFilename = Constants.Certificates.JwtCertificateFilename,
                CertificatePassword = Constants.Certificates.JwtCertificatePassword,
                Issuer = Constants.SoftwareProducts.SoftwareProductId.ToLower(),
                Audience = url
            }.Generate();

            // Post the request
            var api = _apiServiceDirector.BuildDataholderRegisterAPI(accessToken, registrationRequest, HttpMethod.Post);
            var response = await api.SendAsync();

            return response;
        }

        // Get SSA from the Register and register it with the DataHolder
        public async Task<(string ssa, string registration, string clientId)> RegisterSoftwareProduct(
            string brandId = Constants.Brands.BrandId,
            string softwareProductId = Constants.SoftwareProducts.SoftwareProductId,
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword,
            string responseType = "code id_token",
            string authorizationSignedResponseAlg = "PS256")
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(RegisterSoftwareProduct), nameof(DataHolderRegisterService));

            // Get SSA from Register
            var ssa = await _registerSSAService.GetSSA(brandId, softwareProductId, _latestSSAVersion, jwtCertificateFilename, jwtCertificatePassword);

            // Register software product with DataHolder
            var registrationRequest = CreateRegistrationRequest(ssa,
                jwtCertificateFilename: jwtCertificateFilename,
                jwtCertificatePassword: jwtCertificatePassword,
                responseType: responseType,
                authorizationSignedResponseAlg: authorizationSignedResponseAlg);

            var response = await RegisterSoftwareProduct(registrationRequest);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new InvalidOperationException($"Unable to register software product - {softwareProductId} - {response.StatusCode} - {await response.Content.ReadAsStringAsync()}").Log();
            }

            string registration = await response.Content.ReadAsStringAsync();

            // Extract clientId from registration
            dynamic? clientId = JsonConvert.DeserializeObject<dynamic>(registration)?.client_id.ToString();
            if (string.IsNullOrEmpty(clientId))
            {
                throw new InvalidOperationException($"Parameter: {nameof(clientId)} should not be empty.").Log();
            }

            _options.LastRegisteredClientId = clientId;
            return (ssa, registration, clientId);
        }
    }
}
