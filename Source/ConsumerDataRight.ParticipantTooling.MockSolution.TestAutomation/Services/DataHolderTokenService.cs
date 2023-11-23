using System.Net;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public partial class DataHolderTokenService : IDataHolderTokenService
    {
        private readonly TestAutomationOptions _options;
        private readonly TestAutomationAuthServerOptions _authServeroptions;

        public DataHolderTokenService(IOptions<TestAutomationOptions> options, IOptions<TestAutomationAuthServerOptions> authServerOptions)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _authServeroptions = authServerOptions.Value ?? throw new ArgumentNullException(nameof(authServerOptions));
        }

        // Send token request, returning HttpResponseMessage
        public async Task<HttpResponseMessage> SendRequest(
        string? authCode = null,
            bool usePut = false,
            string grantType = "authorization_code",
            string? clientId = null,
            string? issuerClaim = null,
            string clientAssertionType = Constants.ClientAssertionType,
            bool useClientAssertion = true,
            int? shareDuration = null,
            string? refreshToken = null,
            string? customClientAssertion = null,
            string? scope = null,
            string? redirectUri = null,
            string certificateFilename = Constants.Certificates.CertificateFilename,
            string certificatePassword = Constants.Certificates.CertificatePassword,
            string jwkCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwkCertificatePassword = Constants.Certificates.JwtCertificatePassword
            )
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(SendRequest), nameof(DataHolderTokenService));

            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = _options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
            }

            if (clientId == null)
            {
                clientId = _options.LastRegisteredClientId;
            }

            if (issuerClaim == null)
            {
                issuerClaim = _options.LastRegisteredClientId;
            }

            var URL = $"{_options.DH_MTLS_GATEWAY_URL}/connect/token";

            var formFields = new List<KeyValuePair<string?, string?>>
            {
                new KeyValuePair<string?, string?>("redirect_uri", redirectUri),
            };

            if (authCode != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("code", authCode));
            }

            if (grantType != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("grant_type", grantType));
            }

            if (clientId != Constants.Omit)
            {
                formFields.Add(new KeyValuePair<string?, string?>("client_id", clientId?.ToLower() ?? throw new InvalidOperationException($"{nameof(clientId)} can not be null unless intentionally omitted.").Log()));
            }

            if (clientAssertionType != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("client_assertion_type", clientAssertionType));
            }

            if (customClientAssertion != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("client_assertion", customClientAssertion));
            }
            else if (useClientAssertion) //only check if we haven't provided custom client assertion
            {
                var clientAssertion = new PrivateKeyJwtService
                {
                    CertificateFilename = jwkCertificateFilename,
                    CertificatePassword = jwkCertificatePassword,

                    // Allow for clientId to be deliberately omitted from the JWT
                    Issuer = issuerClaim == Constants.Omit ? "" : issuerClaim ?? throw new InvalidOperationException($"{nameof(issuerClaim)} can not be empty unless intentionally omitted.").Log(),

                    // Don't check for issuer if we are deliberately omitting clientId
                    RequireIssuer = clientId != Constants.Omit,

                    Audience = URL
                }.Generate();

                formFields.Add(new KeyValuePair<string?, string?>("client_assertion", clientAssertion));
            }

            if (shareDuration != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("share_duration", shareDuration.ToString()));
            }

            if (refreshToken != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("refresh_token", refreshToken));
            }

            if (scope != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("scope", scope));
            }

            formFields.Add(new KeyValuePair<string?, string?>("code_verifier", Constants.AuthServer.FapiPhase2CodeVerifier));

            var content = new FormUrlEncodedContent(formFields);

            Log.Information("Sending token request:\n {content}\n to {endpoint}.", content.ReadAsStringAsync().Result, URL);

            using var client = Helpers.Web.CreateHttpClient(
                certificateFilename ?? throw new ArgumentNullException(nameof(certificateFilename)),
                certificatePassword);

            Helpers.AuthServer.AttachHeadersForStandAlone(URL, content.Headers, _options.DH_MTLS_GATEWAY_URL, _authServeroptions.XTLSCLIENTCERTTHUMBPRINT, _authServeroptions.STANDALONE);

            var responseMessage = usePut == true ?
                await client.PutAsync(URL, content) :
                await client.PostAsync(URL, content);

            Log.Information("Response from endpoint:\n {content}", responseMessage.Content.ReadAsStringAsync().Result);

            return responseMessage;
        }

        /// <summary>
        /// Use authCode to get access token
        /// </summary>
        public async Task<string?> GetAccessToken(string authCode)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(GetAccessToken), nameof(DataHolderTokenService));

            var responseMessage = await SendRequest(authCode);
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"{nameof(DataHolderTokenService)}.{nameof(GetAccessToken)} - Error getting access token - {responseMessage.StatusCode} - {await responseMessage.Content.ReadAsStringAsync()}").Log();
            }

            var tokenResponse = await JsonExtensions.DeserializeResponseAsync<TokenResponse?>(responseMessage);
            return tokenResponse?.AccessToken;
        }

        /// <summary>
        /// Use authCode to get tokens. 
        /// </summary>
        public async Task<TokenResponse?> GetResponse(string authCode, int? shareDuration = null,
            string? clientId = null,
            string? redirectUri = null,
            string? certificateFilename = Constants.Certificates.CertificateFilename,
            string? certificatePassword = Constants.Certificates.CertificatePassword,
            string? jwkCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string? jwkCertificatePassword = Constants.Certificates.JwtCertificatePassword,
            string? scope = null //added for auth server tests
            )
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(GetResponse), nameof(DataHolderTokenService));

            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = _options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
            }

            if (clientId == null) 
            {
                clientId = _options.LastRegisteredClientId;
            }

            var responseMessage = await SendRequest(authCode, shareDuration: shareDuration,
                clientId: clientId,
                issuerClaim: clientId,
                redirectUri: redirectUri,
                certificateFilename: certificateFilename,
                certificatePassword: certificatePassword,
                jwkCertificateFilename: jwkCertificateFilename,
                jwkCertificatePassword: jwkCertificatePassword,
                scope: scope
            );

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"{nameof(DataHolderTokenService)}.{nameof(GetResponse)} - Error getting response - {responseMessage.StatusCode} - {await responseMessage.Content.ReadAsStringAsync()}").Log();
            }

            var response = await DeserializeResponse(responseMessage);

            return response;
        }

        /// <summary>
        /// Use refresh token to get tokens
        /// </summary>
        public async Task<TokenResponse?> GetResponseUsingRefreshToken(string? refreshToken, string? scope = null)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(GetResponseUsingRefreshToken), nameof(DataHolderTokenService));

            _ = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));

            var tokenResponseMessage = await SendRequest(
                grantType: "refresh_token",
                refreshToken: refreshToken,
                scope: scope
            );

            if (tokenResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                var content = await tokenResponseMessage.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{nameof(DataHolderTokenService)}.{nameof(GetResponseUsingRefreshToken)} - Error getting response - {content}").Log();
            }

            var tokenResponse = await DeserializeResponse(tokenResponseMessage);

            return tokenResponse;
        }

        public async Task<TokenResponse?> DeserializeResponse(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseContent))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
        }
    }
}
