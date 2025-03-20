using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.APIs
{
    /// <summary>
    /// Url for Authorise endpoint.
    /// </summary>
    public class AuthoriseUrl
    {
        public string ClientId { get; private set; } 

        public string RedirectURI { get; private set; }

        public string JwtCertificateFilename { get; private set; } = Constants.Certificates.JwtCertificateFilename;

        public string JwtCertificatePassword { get; private set; } = Constants.Certificates.JwtCertificatePassword;

        public string Scope { get; private set; }

        public string ResponseType { get; private set; }

        public string? Request { get; private set; } = null; // use this as the request, rather than build request

        public string? RequestUri { get; private set; } = null;

        public string Url { get; private set; }

        /// <summary>
        /// Lifetime (in seconds) of the access token. It has to be less than 60 mins.
        /// </summary>
        public int TokenLifetime { get; private set; } = Constants.AuthServer.DefaultTokenLifetime;

        /// <summary>
        /// Lifetime (in seconds) of the CDR arrangement.
        /// 7776000 = 90 days.
        /// </summary>
        public int? SharingDuration { get; private set; } = Constants.AuthServer.SharingDuration;

        private TestAutomationOptions TestAutomationOptions { get; set; }

        public class AuthoriseUrlBuilder : IBuilder<AuthoriseUrl>
        {
            private readonly AuthoriseUrl _authoriseUrl = new AuthoriseUrl();

            public AuthoriseUrlBuilder(TestAutomationOptions options)
            {
                _authoriseUrl.TestAutomationOptions = options ?? throw new ArgumentNullException(nameof(options)).Log();
            }

            public AuthoriseUrlBuilder WithClientId(string value)
            {
                _authoriseUrl.ClientId = value;
                return this;
            }

            public AuthoriseUrlBuilder WithRedirectURI(string value)
            {
                _authoriseUrl.RedirectURI = value;
                return this;
            }

            public AuthoriseUrlBuilder WithJWTCertificateFilename(string value)
            {
                _authoriseUrl.JwtCertificateFilename = value;
                return this;
            }

            public AuthoriseUrlBuilder WithJWTCertificatePassword(string value)
            {
                _authoriseUrl.JwtCertificatePassword = value;
                return this;
            }

            public AuthoriseUrlBuilder WithScope(string value)
            {
                _authoriseUrl.Scope = value;
                return this;
            }

            public AuthoriseUrlBuilder WithResponseType(string value)
            {
                _authoriseUrl.ResponseType = value;
                return this;
            }

            public AuthoriseUrlBuilder WithRequest(string value)
            {
                _authoriseUrl.Request = value;
                return this;
            }

            public AuthoriseUrlBuilder WithRequestUri(string? value)
            {
                _authoriseUrl.RequestUri = value;
                return this;
            }

            public AuthoriseUrl Build()
            {
                Log.Information("Building a {BuiltClass} using {BuilderClass}.", nameof(AuthoriseUrl), nameof(AuthoriseUrlBuilder));

                FillMissingDefaults();
                _authoriseUrl.Url = GenerateUrl();
                return _authoriseUrl;
            }

            private string GenerateUrl()
            {
                var queryString = new Dictionary<string, string?>
                {
                    { "client_id", _authoriseUrl.ClientId }
                };

                if (_authoriseUrl.ResponseType != null)
                {
                    queryString.Add("response_type", _authoriseUrl.ResponseType);
                }

                if (_authoriseUrl.RequestUri != null)
                {
                    queryString.Add("request_uri", _authoriseUrl.RequestUri);
                }
                else
                {
                    queryString.Add("request", _authoriseUrl.Request ?? CreateRequest());
                }

                var url = QueryHelpers.AddQueryString($"{_authoriseUrl.TestAutomationOptions.DH_TLS_AUTHSERVER_BASE_URL}/connect/authorize", queryString);

                return url;
            }

            private void FillMissingDefaults()
            {
                Log.Information("Filling missing defaults for public properties");

                var _options = _authoriseUrl.TestAutomationOptions; // purely to shorten reference

                // Anything that doesn't have a value which should...will get it's default value set
                if (_authoriseUrl.Scope.IsNullOrWhiteSpace())
                {
                    _authoriseUrl.Scope = _options.SCOPE;
                }

                if (_authoriseUrl.RedirectURI.IsNullOrWhiteSpace())
                {
                    _authoriseUrl.RedirectURI = _options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
                }

                if (_authoriseUrl.ResponseType.IsNullOrWhiteSpace())
                {
                    _authoriseUrl.ResponseType = Enums.ResponseType.CodeIdToken.ToEnumMemberAttrValue();
                }
            }

            private string CreateRequest()
            {
                var iat = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

                var subject = new Dictionary<string, object>
                {
                    { "iss", _authoriseUrl.ClientId },
                    { "iat", iat },
                    { "nbf", iat },
                    { "exp", iat + _authoriseUrl.TokenLifetime },
                    { "jti", Guid.NewGuid().ToString().Replace("-", string.Empty) },
                    { "aud", _authoriseUrl.TestAutomationOptions.DH_TLS_AUTHSERVER_BASE_URL },
                    { "response_type", _authoriseUrl.ResponseType },
                    { "client_id", _authoriseUrl.ClientId },
                    { "redirect_uri", _authoriseUrl.RedirectURI },
                    { "scope", _authoriseUrl.Scope },
                    { "state", "foo" },
                    { "nonce", "foo" },
                    {
                        "claims", new
                    {
                        sharing_duration = _authoriseUrl.SharingDuration.ToString(),
                        id_token = new
                        {
                            acr = new
                            {
                                essential = true,
                                values = new string[] { "urn:cds.au:cdr:2" }
                            }
                        }
                    }
                    }
                };

                return Helpers.Jwt.CreateJWT(_authoriseUrl.JwtCertificateFilename, _authoriseUrl.JwtCertificatePassword, subject);
            }
        }
    }
}