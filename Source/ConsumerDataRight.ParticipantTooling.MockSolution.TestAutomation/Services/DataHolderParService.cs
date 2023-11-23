using System.Net;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public class DataHolderParService : IDataHolderParService
    {
        private readonly TestAutomationOptions _options;
        private readonly TestAutomationAuthServerOptions _authServerOptions;

        public DataHolderParService(IOptions<TestAutomationOptions> options, IOptions<TestAutomationAuthServerOptions> authServerOptions)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _authServerOptions = authServerOptions.Value ?? throw new ArgumentNullException(nameof(authServerOptions));
        }

        public class Response
        {
            [JsonProperty("request_uri")]
            public string? RequestURI { get; set; }

            [JsonProperty("expires_in")]
            public int? ExpiresIn { get; set; }
        };

        public async Task<HttpResponseMessage> SendRequest(
             string? scope,
             string? clientId = null,
             string clientAssertionType = Constants.ClientAssertionType,
             int? sharingDuration = Constants.AuthServer.SharingDuration,
             string? aud = null,
             int nbfOffsetSeconds = 0,
             int expOffsetSeconds = 0,
             bool addRequestObject = true,
             bool addNotBeforeClaim = true,
             bool addExpiryClaim = true,
             string? cdrArrangementId = null,
             string? redirectUri = null,
             string? clientAssertion = null,

             string codeVerifier = Constants.AuthServer.FapiPhase2CodeVerifier,
             string codeChallengeMethod = Constants.AuthServer.FapiPhase2CodeChallengeMethod,

             string? requestUri = null,
             ResponseMode? responseMode = ResponseMode.Fragment,
             string certificateFilename = Constants.Certificates.CertificateFilename,
             string certificatePassword = Constants.Certificates.CertificatePassword,
             string jwtCertificateForClientAssertionFilename = Constants.Certificates.JwtCertificateFilename,
             string jwtCertificateForClientAssertionPassword = Constants.Certificates.JwtCertificatePassword,
             string jwtCertificateForRequestObjectFilename = Constants.Certificates.JwtCertificateFilename,
             string jwtCertificateForRequestObjectPassword = Constants.Certificates.JwtCertificatePassword,

             ResponseType? responseType = ResponseType.CodeIdToken,
             string? state = null)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(SendRequest), nameof(DataHolderParService));

            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = _options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
            }

            if (clientId == null)
            {
                clientId = _options.LastRegisteredClientId;
            }

            var issuer = _options.DH_TLS_AUTHSERVER_BASE_URL;

            var parUrl = $"{_options.CDRAUTHSERVER_SECUREBASEURI}/connect/par";

            var formFields = new List<KeyValuePair<string?, string?>>();

            if (clientAssertionType != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("client_assertion_type", clientAssertionType));
            }

            if (requestUri != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("request_uri", requestUri));
            }

            formFields.Add(new KeyValuePair<string?, string?>("client_assertion", clientAssertion ??
                new PrivateKeyJwtService()
                {
                    CertificateFilename = jwtCertificateForClientAssertionFilename,
                    CertificatePassword = jwtCertificateForClientAssertionPassword,
                    Issuer = clientId ?? throw new NullReferenceException(nameof(clientId)),
                    Audience = aud ?? issuer
                }.Generate()
            ));

            if (addRequestObject)
            {
                var iat = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

                var requestObject = new Dictionary<string, object> {
                    { "iss", clientId ?? throw new NullReferenceException(nameof(clientId))},
                    { "iat", iat },
                    { "jti", Guid.NewGuid().ToString().Replace("-", string.Empty) },

                    { "response_mode",  responseMode?.ToEnumMemberAttrValue()},
                    { "aud", aud ?? _options.DH_TLS_AUTHSERVER_BASE_URL },
                    { "client_id", clientId },
                    { "redirect_uri", redirectUri },
                    { "state", state ?? Guid.NewGuid().ToString() },
                    { "nonce", Guid.NewGuid().ToString() },
                    { "claims", new {
                        sharing_duration = $"{sharingDuration}",
                        cdr_arrangement_id = cdrArrangementId,
                        id_token = new {
                            acr = new {
                                essential = true,
                                values = new string[] { "urn:cds.au:cdr:2" }
                                }
                            },
                        }
                    }
                };

                if (addNotBeforeClaim)
                {
                    requestObject.Add("nbf", iat + nbfOffsetSeconds);
                }

                if (addExpiryClaim)
                {
                    requestObject.Add("exp", iat + expOffsetSeconds + 600);
                }

                if (!scope.IsNullOrWhiteSpace())
                {
                    requestObject.Add("scope", scope);
                }

                if (codeVerifier != null)
                {
                    requestObject.Add("code_challenge", codeVerifier.CreatePkceChallenge());
                }

                if (codeChallengeMethod != null)
                {
                    requestObject.Add("code_challenge_method", codeChallengeMethod);
                }

                if (responseType != null)
                {
                    requestObject.Add("response_type", responseType.ToEnumMemberAttrValue());
                }

                var jwt = Helpers.Jwt.CreateJWT(jwtCertificateForRequestObjectFilename ?? Constants.Certificates.JwtCertificateFilename, jwtCertificateForRequestObjectPassword ?? Constants.Certificates.JwtCertificatePassword, requestObject);

                formFields.Add(new KeyValuePair<string?, string?>("request", jwt));
            }

            var content = new FormUrlEncodedContent(formFields);

            using var client = Helpers.Web.CreateHttpClient(
                certificateFilename ?? throw new ArgumentNullException(nameof(certificateFilename)),
                certificatePassword);

            Helpers.AuthServer.AttachHeadersForStandAlone(parUrl, content.Headers, _options.DH_MTLS_GATEWAY_URL, _authServerOptions.XTLSCLIENTCERTTHUMBPRINT, _authServerOptions.STANDALONE);

            var responseMessage = await client.PostAsync(parUrl, content);

            return responseMessage;
        }

        public async Task<string> GetRequestUri(
                   string? scope,
                   string? clientId = null,
                   string jwtCertificateForClientAssertionFilename = Constants.Certificates.JwtCertificateFilename,
                   string jwtCertificateForClientAssertionPassword = Constants.Certificates.JwtCertificatePassword,
                   string jwtCertificateForRequestObjectFilename = Constants.Certificates.JwtCertificateFilename,
                   string jwtCertificateForRequestObjectPassword = Constants.Certificates.JwtCertificatePassword,
                   string? redirectUri = null,
                   int? sharingDuration = Constants.AuthServer.SharingDuration,
                   string? cdrArrangementId = null,
                   ResponseMode responseMode = ResponseMode.Fragment)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(GetRequestUri), nameof(DataHolderParService));

            if (clientId == null)
            {
                clientId = _options.LastRegisteredClientId;
            }

            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = _options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
            }

            var response = await SendRequest(
                scope: scope,
                clientId: clientId,
                jwtCertificateForClientAssertionFilename: jwtCertificateForClientAssertionFilename,
                jwtCertificateForClientAssertionPassword: jwtCertificateForClientAssertionPassword,
                jwtCertificateForRequestObjectFilename: jwtCertificateForRequestObjectFilename,
                jwtCertificateForRequestObjectPassword: jwtCertificateForRequestObjectPassword,
                redirectUri: redirectUri,
                sharingDuration: sharingDuration,
                cdrArrangementId: cdrArrangementId,
                responseMode: responseMode);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
            {
                throw new InvalidOperationException($"Statuscode={response.StatusCode} - Response.Content={await response.Content.ReadAsStringAsync()}").Log();
            }

            var json = await JsonExtensions.DeserializeResponseAsync<Response?>(response);

            var requestUri = json?.RequestURI ?? throw new NullReferenceException("requestUri");

            return requestUri;
        }

        public async Task<Response?> DeserializeResponse(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseContent))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Response>(responseContent);
        }
    }
}
