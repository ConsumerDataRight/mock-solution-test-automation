namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    using System.Net.Http.Headers;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
    using Serilog;

    /// <summary>
    /// Call API.
    /// </summary>
    public class ApiService : IApiService
    {
        /// <summary>
        /// Filename of certificate to use.
        /// If null then no certificate will be attached to the request.
        /// </summary>
        public string? CertificateFilename { get; private set; } = Constants.Certificates.CertificateFilename;

        /// <summary>
        /// Password for certificate.
        /// If null then no certificate password will be set.
        /// </summary>
        public string? CertificatePassword { get; private set; } = Constants.Certificates.CertificatePassword;

        /// <summary>
        /// Access token.
        /// If null then no access token will be attached to the request.
        /// See the AccessToken class to generate an access token.
        /// </summary>
        public string? AccessToken { get; private set; }

        /// <summary>
        /// The HttpMethod of the request.
        /// </summary>
        public HttpMethod? HttpMethod { get; private set; }

        /// <summary>
        /// The URL of the request.
        /// </summary>
        public string? URL { get; private set; }

        /// <summary>
        /// The x-v header.
        /// If null then no x-v header will be set.
        /// </summary>
        public string? XV { get; private set; }

        /// <summary>
        /// The x-min-v header.
        /// If null then no x-min-v header will be set.
        /// </summary>
        public string? XMinV { get; private set; }

        /// <summary>
        /// The If-None-Match header (an ETag).
        /// If null then no If-None-Match header will be set.
        /// </summary>
        public string? IfNoneMatch { get; private set; }

        /// <summary>
        /// The x_fapi_auth_date header.
        /// If null then no x_fapi_auth_date header will be set.
        /// </summary>
        public string? XFapiAuthDate { get; private set; }

        /// <summary>
        /// The x-fapi-interaction-id header.
        /// If null then no x-fapi-interaction-id header will be set.
        /// </summary>
        public string? XFapiInteractionId { get; private set; }

        /// <summary>
        /// Content
        /// If null then no content is set.
        /// </summary>
        public HttpContent? Content { get; private set; }

        /// <summary>
        /// Content.Headers.ContentType
        /// If null then Content.Headers.ContentType is not set.
        /// </summary>
        public MediaTypeHeaderValue? ContentType { get; private set; }

        /// <summary>
        /// Request.Headers.Accept
        /// If null then Request.Headers.Accept is not set.
        /// </summary>
        public string? Accept { get; private set; }

        public IEnumerable<string>? Cookies { get; private set; }

        /// <summary>
        /// Set authentication header explicity. Can't be used if AccessToken is set.
        /// </summary>
        public AuthenticationHeaderValue? AuthenticationHeaderValue { get; }

        /// <summary>
        /// Running standalone CdrAuthServer (ie no MtlsGateway).
        /// </summary>
        public bool IsStandalone { get; private set; } = false;

        /// <summary>
        /// Dh Mtls Gateway Url
        /// Only needed if isStandalone is true.
        /// </summary>
        public string? DhMtlsGatewayUrl { get; private set; }

        /// <summary>
        /// Xtls Client Certificate Thumbprint
        /// Only needed if isStandalone is true.
        /// </summary>
        public string? XtlsClientCertificateThumbprint { get; private set; }

        private ApiService()
        {
        } // restricts class instantiation to builder only

        /// <summary>
        /// Send a request to the API.
        /// </summary>
        /// <returns>The API response.</returns>
        public async Task<HttpResponseMessage> SendAsync(bool allowAutoRedirect = true, string? xtlsThumbprint = null)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(SendAsync), nameof(ApiService));

            var request = BuildRequest();
            AttachHeaders(request);

            return await SendRequest(request, allowAutoRedirect);
        }

        private HttpRequestMessage BuildRequest()
        {
            EnsureHttpMethodAndUrlAreSet();

            var request = new HttpRequestMessage(HttpMethod, URL);

            AddAuthorizationHeader(request);
            AddOptionalHeaders(request);
            SetContent(request);

            return request;
        }

        private void EnsureHttpMethodAndUrlAreSet()
        {
            if (HttpMethod == null)
            {
                throw new InvalidOperationException($"{nameof(ApiService)}.{nameof(SendAsync)}.{nameof(BuildRequest)} - {nameof(HttpMethod)} not set").Log();
            }

            if (URL == null)
            {
                throw new InvalidOperationException($"{nameof(ApiService)}.{nameof(SendAsync)}.{nameof(BuildRequest)} - {nameof(URL)} not set").Log();
            }
        }

        private void AddAuthorizationHeader(HttpRequestMessage request)
        {
            if (AccessToken != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            if (AuthenticationHeaderValue != null)
            {
                if (AccessToken != null)
                {
                    throw new InvalidOperationException($"{nameof(ApiService)}.{nameof(SendAsync)} - Can't use both AccessToken and AuthenticationHeaderValue.").Log();
                }

                request.Headers.Authorization = AuthenticationHeaderValue;
            }
        }

        private void AddOptionalHeaders(HttpRequestMessage request)
        {
            AddHeader(request, "x-v", XV);
            AddHeader(request, "x-min-v", XMinV);
            AddHeader(request, "If-None-Match", IfNoneMatch, format: value => $"\"{value}\"");
            AddHeader(request, "x-fapi-auth-date", XFapiAuthDate);
            AddHeader(request, "x-fapi-interaction-id", XFapiInteractionId);
            AddHeader(request, "Accept", Accept);
        }

        private static void AddHeader(HttpRequestMessage request, string headerName, string? headerValue, Func<string, string>? format = null)
        {
            if (headerValue != null)
            {
                request.Headers.Add(headerName, format != null ? format(headerValue) : headerValue);
            }
        }

        private void SetContent(HttpRequestMessage request)
        {
            if (Content != null)
            {
                request.Content = Content;

                if (ContentType != null)
                {
                    request.Content.Headers.ContentType = ContentType;
                }
            }
        }

        private void AttachHeaders(HttpRequestMessage request)
        {
            Helpers.AuthServer.AttachHeadersForStandAlone(
                request.RequestUri?.AbsoluteUri ?? throw new InvalidOperationException("AbsoluteUri is null"),
                request.Headers, DhMtlsGatewayUrl, XtlsClientCertificateThumbprint, IsStandalone);
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, bool allowAutoRedirect)
        {
            using var client = Helpers.Web.CreateHttpClient(CertificateFilename, CertificatePassword, allowAutoRedirect, Cookies, request);
            return await client.SendAsync(request);
        }

        public class ApiServiceBuilder : IBuilder<ApiService>
        {
            public ApiServiceBuilder()
            {
            }

            private readonly ApiService _api = new ApiService();

            public ApiServiceBuilder WithUrl(string? value)
            {
                _api.URL = value;
                return this;
            }

            public ApiServiceBuilder WithXV(string? value)
            {
                _api.XV = value;
                return this;
            }

            public ApiServiceBuilder WithXFapiAuthDate(string? value)
            {
                _api.XFapiAuthDate = value;
                return this;
            }

            public ApiServiceBuilder WithAccessToken(string? value)
            {
                _api.AccessToken = value;
                return this;
            }

            public ApiServiceBuilder WithXFapiInteractionId(string? value)
            {
                _api.XFapiInteractionId = value;
                return this;
            }

            public ApiServiceBuilder WithHttpMethod(HttpMethod? value)
            {
                _api.HttpMethod = value;
                return this;
            }

            public ApiServiceBuilder WithContent(HttpContent? value)
            {
                _api.Content = value;
                return this;
            }

            public ApiServiceBuilder WithContentType(MediaTypeHeaderValue? value)
            {
                _api.ContentType = value;
                return this;
            }

            public ApiServiceBuilder WithAccept(string? value)
            {
                _api.Accept = value;
                return this;
            }

            public ApiServiceBuilder WithXMinV(string? value)
            {
                _api.XMinV = value;
                return this;
            }

            public ApiServiceBuilder WithIfNoneMatch(string? value)
            {
                _api.IfNoneMatch = value;
                return this;
            }

            public ApiServiceBuilder WithCookies(IEnumerable<string>? value)
            {
                _api.Cookies = value;
                return this;
            }

            public ApiServiceBuilder WithCertificateFilename(string? value)
            {
                _api.CertificateFilename = value;
                return this;
            }

            public ApiServiceBuilder WithCertificatePassword(string? value)
            {
                _api.CertificatePassword = value;
                return this;
            }

            public ApiServiceBuilder WithIsStandalone(bool value)
            {
                _api.IsStandalone = value;
                return this;
            }

            public ApiServiceBuilder WithDhMtlsGatewayUrl(string? value)
            {
                _api.DhMtlsGatewayUrl = value;
                return this;
            }

            public ApiServiceBuilder WithXtlsClientCertificateThumbprint(string? value)
            {
                _api.XtlsClientCertificateThumbprint = value;
                return this;
            }

            public ApiService Build()
            {
                Log.Information("Building a {BuiltClass} using {BuilderClass}.", nameof(ApiService), nameof(ApiServiceBuilder));
                return _api;
            }
        }
    }
}
