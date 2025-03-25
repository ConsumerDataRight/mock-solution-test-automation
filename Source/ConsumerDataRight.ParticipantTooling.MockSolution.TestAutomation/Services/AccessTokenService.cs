namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
    using Serilog;

    public class AccessTokenService : IAccessTokenService
    {
        public AccessTokenService(string mtlsAuthServerTokenUrl)
        {
            URL = mtlsAuthServerTokenUrl;
            Audience = mtlsAuthServerTokenUrl;
        }

        private const string _defaultClientId = "86ecb655-9eba-409c-9be3-59e7adf7080d";

        public string? CertificateFilename { get; set; }

        public string? CertificatePassword { get; set; }

        public string? JwtCertificateFilename { get; set; }

        public string? JwtCertificatePassword { get; set; }

        public string URL { get; init; }

        public string Issuer { get; init; } = _defaultClientId;

        public string Audience { get; init; }

        public string Scope { get; init; } = "bank:accounts.basic:read";

        public string GrantType { get; init; } = string.Empty;

        public string ClientId { get; init; } = _defaultClientId;

        public string ClientAssertionType { get; init; } = string.Empty;

        /// <summary>
        /// Get HttpRequestMessage for access token request.
        /// </summary>
        private static HttpRequestMessage CreateAccessTokenRequest(
           string url,
           string jwtCertificateFilename, string jwtCertificatePassword,
           string issuer, string audience,
           string scope, string grantType, string clientId, string clientAssertionType)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(CreateAccessTokenRequest), nameof(AccessTokenService));

            static string BuildContent(string scope, string grant_type, string client_id, string client_assertion_type, string client_assertion)
            {
                var kvp = new KeyValuePairBuilder();

                if (scope != null)
                {
                    kvp.Add("scope", scope);
                }

                if (grant_type != null)
                {
                    kvp.Add("grant_type", grant_type);
                }

                if (client_id != null)
                {
                    kvp.Add("client_id", client_id);
                }

                if (client_assertion_type != null)
                {
                    kvp.Add("client_assertion_type", client_assertion_type);
                }

                if (client_assertion != null)
                {
                    kvp.Add("client_assertion", client_assertion);
                }

                return kvp.Value;
            }

            var client_assertion = new PrivateKeyJwtService
            {
                CertificateFilename = jwtCertificateFilename,
                CertificatePassword = jwtCertificatePassword,
                Issuer = issuer,
                Audience = audience,
            }.Generate();

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(
                    BuildContent(scope, grantType, clientId, clientAssertionType, client_assertion),
                    Encoding.UTF8,
                    "application/json"),
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            return request;
        }

        /// <summary>
        /// Get an access token from Auth Server.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task<string?> GetAsync(string dhMtlsGatewayUrl, string xtlsClientCertThumbprint, bool isStandalone)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(GetAsync), nameof(AccessTokenService));

            // Create HttpClient
            using var client = Helpers.Web.CreateHttpClient(CertificateFilename, CertificatePassword);

            // Create an access token request
            var request = CreateAccessTokenRequest(
                URL,
                JwtCertificateFilename ?? throw new InvalidOperationException($"{nameof(JwtCertificateFilename)} is null").Log(),
                JwtCertificatePassword,
                Issuer, Audience,
                Scope, GrantType, ClientId, ClientAssertionType);

            Helpers.AuthServer.AttachHeadersForStandAlone(request.RequestUri?.AbsoluteUri ?? throw new InvalidOperationException($"{nameof(request.RequestUri.AbsoluteUri)} is null").Log(), request.Headers, dhMtlsGatewayUrl, xtlsClientCertThumbprint, isStandalone);

            // Request the access token
            var response = await client.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"{nameof(AccessTokenService)}.{nameof(GetAsync)} - Error getting access token - {response.StatusCode} - {await response.Content.ReadAsStringAsync()}").Log();
            }

            // Deserialize the access token from the response
            var accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.AccessToken>(await response.Content.ReadAsStringAsync());

            // And return the access token
            return accessToken?.Token;
        }
    }
}
