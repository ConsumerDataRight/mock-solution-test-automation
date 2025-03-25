namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Web;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.APIs;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation;
    using Dapper;
    using FluentAssertions;
    using HtmlAgilityPack;
    using Jose;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Data.SqlClient;
    using Microsoft.Playwright;
    using Serilog;

    public partial class DataHolderAuthoriseService : IDataHolderAuthoriseService
    {
        private DataHolderAuthoriseService()
        {
        } // private constructor ensures that the builder must be used to instantiate a new service

        private string[]? SelectedAccountDisplayNamesCache { get; set; } = null;

        private string? AuthoriseUrl { get; set; }

        private TestAutomationOptions TestAutomationOptions { get; set; }

        private TestAutomationAuthServerOptions? AuthServerOptions { get; set; }

        private IApiServiceDirector? ApiDirector { get; set; }

        /// <summary>
        /// The customer's userid with the DataHolder - eg "jwilson".
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// The OTP (One-time password) that is sent to the customer (via sms etc) so the DataHolder can authenticate the Customer.
        /// For the mock solution use "000789".
        /// </summary>
        public string OTP { get; private set; } = Constants.AuthoriseOTP;

        ///// <summary>
        ///// Comma delimited list of account ids the user is granting consent for
        ///// </summary>
        public string? SelectedAccountIds { get; private set; }

        protected string[]? SelectedAccountIdsArray => SelectedAccountIds?.Split(",");

        /// <summary>
        /// Scope.
        /// </summary>
        public string Scope { get; private set; }

        /// <summary>
        /// Lifetime (in seconds) of the access token.
        /// </summary>
        public int TokenLifetime { get; private set; } = Constants.AuthServer.DefaultTokenLifetime;

        /// <summary>
        /// Lifetime (in seconds) of the CDR arrangement.
        /// SHARING_DURATION = 90 days.
        /// </summary>
        public int? SharingDuration { get; private set; } = Constants.AuthServer.SharingDuration;

        public string? RequestUri { get; private set; }

        public string CertificateFilename { get; private set; }

        public string CertificatePassword { get; private set; }

        public string ClientId { get; private set; }

        public string RedirectURI { get; private set; }

        public string JwtCertificateFilename { get; private set; }

        public string JwtCertificatePassword { get; private set; }

        public ResponseType ResponseType { get; private set; } = ResponseType.Code;

        public ResponseMode ResponseMode { get; private set; } = ResponseMode.Jwt;

        public string? CdrArrangementId { get; private set; }

        public class DataHolderAuthoriseServiceBuilder : IAsyncBuilder<DataHolderAuthoriseService>
        {
            public DataHolderAuthoriseServiceBuilder(TestAutomationOptions options, IDataHolderParService dataHolderParService, IApiServiceDirector apiServiceDirector, bool useAdditionalDefaults = false, TestAutomationAuthServerOptions? authServerOptions = null)
            {
                _useAdditionalDefaults = useAdditionalDefaults;
                _dataHolderParService = dataHolderParService ?? throw new ArgumentNullException(nameof(dataHolderParService));

                _dataHolderAuthoriseService.TestAutomationOptions = options ?? throw new ArgumentNullException(nameof(options));
                _dataHolderAuthoriseService.ApiDirector = apiServiceDirector ?? throw new ArgumentNullException(nameof(apiServiceDirector));
                _dataHolderAuthoriseService.AuthServerOptions = authServerOptions; // no null check here because this is nullable
            }

            private readonly DataHolderAuthoriseService _dataHolderAuthoriseService = new DataHolderAuthoriseService();
            private readonly bool _useAdditionalDefaults;
            private readonly IDataHolderParService _dataHolderParService;

            public DataHolderAuthoriseServiceBuilder WithUserId(string value)
            {
                _dataHolderAuthoriseService.UserId = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithOTP(string value)
            {
                _dataHolderAuthoriseService.OTP = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithSelectedAccountIds(string value)
            {
                _dataHolderAuthoriseService.SelectedAccountIds = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithScope(string value)
            {
                _dataHolderAuthoriseService.Scope = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithTokenLifetime(int value)
            {
                _dataHolderAuthoriseService.TokenLifetime = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithSharingDuration(int? value)
            {
                _dataHolderAuthoriseService.SharingDuration = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithRequestUri(string value)
            {
                _dataHolderAuthoriseService.RequestUri = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithClientId(string value)
            {
                _dataHolderAuthoriseService.ClientId = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithRedirectUri(string value)
            {
                _dataHolderAuthoriseService.RedirectURI = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithResponseType(ResponseType value)
            {
                _dataHolderAuthoriseService.ResponseType = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithResponseMode(ResponseMode value)
            {
                _dataHolderAuthoriseService.ResponseMode = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithCertificateFilename(string value)
            {
                _dataHolderAuthoriseService.CertificateFilename = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithCertificatePassword(string value)
            {
                _dataHolderAuthoriseService.CertificatePassword = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithJwtCertificateFilename(string value)
            {
                _dataHolderAuthoriseService.JwtCertificateFilename = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithJwtCertificatePassword(string value)
            {
                _dataHolderAuthoriseService.JwtCertificatePassword = value;
                return this;
            }

            public DataHolderAuthoriseServiceBuilder WithCdrArrangementId(string? value)
            {
                _dataHolderAuthoriseService.CdrArrangementId = value;
                return this;
            }

            public async Task<DataHolderAuthoriseService> BuildAsync()
            {
                Log.Information("Building a {BuiltClass} using {BuilderClass}.", nameof(DataHolderAuthoriseService), nameof(DataHolderAuthoriseServiceBuilder));

                var _options = _dataHolderAuthoriseService.TestAutomationOptions; // purely to shorten reference

                FillMissingDefaults();
                Validate();

                if (_options.IS_AUTH_SERVER)
                {
                    if (_dataHolderAuthoriseService.RequestUri.IsNullOrWhiteSpace())
                    {
                        _dataHolderAuthoriseService.RequestUri = await _dataHolderParService.GetRequestUri(
                               scope: _dataHolderAuthoriseService.Scope,
                               sharingDuration: _dataHolderAuthoriseService.SharingDuration,
                               clientId: _dataHolderAuthoriseService.ClientId,
                               cdrArrangementId: _dataHolderAuthoriseService.CdrArrangementId,
                               responseType: _dataHolderAuthoriseService.ResponseType,
                               responseMode: _dataHolderAuthoriseService.ResponseMode);
                    }

                    _dataHolderAuthoriseService.AuthoriseUrl = new AuthoriseUrl.AuthoriseUrlBuilder(_options)
                        .WithScope(_dataHolderAuthoriseService.Scope)
                        .WithRedirectURI(_dataHolderAuthoriseService.RedirectURI)
                        .WithRequestUri(_dataHolderAuthoriseService.RequestUri)
                        .WithClientId(_dataHolderAuthoriseService.ClientId)
                        .WithJWTCertificateFilename(_useAdditionalDefaults ? Constants.Certificates.AdditionalJwksCertificateFilename : Constants.Certificates.JwtCertificateFilename)
                        .WithJWTCertificatePassword(_useAdditionalDefaults ? Constants.Certificates.AdditionalJwksCertificatePassword : Constants.Certificates.JwtCertificatePassword)
                        .WithResponseType(_dataHolderAuthoriseService.ResponseType.ToEnumMemberAttrValue())
                        .Build().Url;
                }
                else
                {
                    if (_dataHolderAuthoriseService.RequestUri.IsNullOrWhiteSpace())
                    {
                        _dataHolderAuthoriseService.RequestUri = await _dataHolderParService.GetRequestUri(
                           scope: _dataHolderAuthoriseService.Scope,
                           sharingDuration: _dataHolderAuthoriseService.SharingDuration,
                           clientId: _dataHolderAuthoriseService.ClientId,
                           responseType: _dataHolderAuthoriseService.ResponseType,
                           responseMode: _dataHolderAuthoriseService.ResponseMode,
                           cdrArrangementId: _dataHolderAuthoriseService.CdrArrangementId);
                    }
                }

                return _dataHolderAuthoriseService;
            }

            private void FillMissingDefaults()
            {
                Log.Information("Filling missing defaults for public properties");

                var options = _dataHolderAuthoriseService.TestAutomationOptions; // purely to shorten reference

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.ClientId, _dataHolderAuthoriseService,
                    () => options.LastRegisteredClientId ?? throw new InvalidOperationException($"{nameof(options.LastRegisteredClientId)} should not be null.").Log(),
                    nameof(ClientId));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.RedirectURI, _dataHolderAuthoriseService,
                    () => _useAdditionalDefaults ? options.ADDITIONAL_SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS : options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS,
                    nameof(RedirectURI));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.Scope, _dataHolderAuthoriseService,
                    () => options.SCOPE,
                    nameof(Scope));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.SelectedAccountIds, _dataHolderAuthoriseService,
                    () => GetAccountIdsForUser(_dataHolderAuthoriseService.UserId),
                    nameof(SelectedAccountIds));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.CertificateFilename, _dataHolderAuthoriseService,
                    () => _useAdditionalDefaults ? Constants.Certificates.AdditionalCertificateFilename : Constants.Certificates.CertificateFilename,
                    nameof(CertificateFilename));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.CertificatePassword, _dataHolderAuthoriseService,
                    () => _useAdditionalDefaults ? Constants.Certificates.AdditionalCertificatePassword : Constants.Certificates.CertificatePassword,
                    nameof(CertificatePassword));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.JwtCertificateFilename, _dataHolderAuthoriseService,
                    () => _useAdditionalDefaults ? Constants.Certificates.AdditionalJwksCertificateFilename : Constants.Certificates.JwtCertificateFilename,
                    nameof(JwtCertificateFilename));

                SetDefaultValueIfEmpty(_dataHolderAuthoriseService.JwtCertificatePassword, _dataHolderAuthoriseService,
                    () => _useAdditionalDefaults ? Constants.Certificates.AdditionalJwksCertificatePassword : Constants.Certificates.JwtCertificatePassword,
                    nameof(JwtCertificatePassword));
            }

            private static void SetDefaultValueIfEmpty(string property, object target, Func<string> getDefaultValue, string propertyName)
            {
                if (property.IsNullOrWhiteSpace())
                {
                    var prop = target.GetType().GetProperty(propertyName);
                    prop.SetValue(target, getDefaultValue());
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", property, propertyName);
                }
            }

            private void Validate()
            {
                Log.Information("Validating {BUILTOBJECT} mandatory properties.", nameof(DataHolderAuthoriseService));
                if (_dataHolderAuthoriseService.RedirectURI.IsNullOrWhiteSpace())
                {
                    throw new InvalidRedirectUriException("RedirectURI is null or empty").Log();
                }

                if (_dataHolderAuthoriseService.ClientId.IsNullOrWhiteSpace())
                {
                    throw new InvalidClientException("ClientId is null or empty").Log();
                }

                if (_dataHolderAuthoriseService.UserId.IsNullOrWhiteSpace())
                {
                    throw new InvalidClientException("UserId is null or empty").Log();
                }
            }

            public static string GetAccountIdsForUser(string userId)
            {
                return userId switch
                {
                    Constants.Users.UserIdKamillaSmith => Constants.Accounts.AccountIdsAllKamillaSmith,
                    Constants.Users.Energy.UserIdMaryMoss => Constants.Accounts.Energy.AccountIdsAllMaryMoss,
                    Constants.Users.Banking.UserIdJaneWilson => Constants.Accounts.Banking.AccountIdsAllJaneWilson,
                    _ => throw new ArgumentException($"{nameof(GetAccountIdsForUser)} - Unsupported user id - {userId}.").Log()
                };
            }
        }

        public async Task<(string authCode, string idToken)> Authorise()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Authorise), nameof(DataHolderAuthoriseService));

            if (TestAutomationOptions.IS_AUTH_SERVER)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.This has been disabled because AuthServerOptions is assigned in the constructor following a null check
                if (AuthServerOptions.HEADLESSMODE)
                {
                    return await AuthoriseHeadless();
                }
                else
                {
                    throw new NotImplementedException($"{nameof(Authorise)} Method only supports headless mode for AuthServer Integration Tests.");
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            else
            {
                Uri authRedirectUri = await Authorise_GetRedirectUri();

                return await Authorize_Consent(authRedirectUri);
            }
        }

        #region AuthServer
        public async Task<HttpResponseMessage> AuthoriseForJarm()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(AuthoriseForJarm), nameof(DataHolderAuthoriseService));

            var callback = new DataRecipientConsentCallback(RedirectURI);
            callback.Start();
            try
            {
                var cookieContainer = new CookieContainer();
                var response = await AuthServer_Authorise(cookieContainer, false) ?? throw new InvalidOperationException("Authorise response is null");
                return response;
            }
            finally
            {
                await callback.Stop();
            }
        }

        /// <summary>
        /// Perform authorisation and consent flow. Returns authCode and idToken.
        /// </summary>
        private async Task<(string authCode, string idToken)> AuthoriseHeadless()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(AuthoriseHeadless), nameof(DataHolderAuthoriseService));

            bool allowRedirects = ResponseType == ResponseType.CodeIdToken;

            var callback = new DataRecipientConsentCallback(RedirectURI);
            callback.Start();
            try
            {
                var cookieContainer = new CookieContainer();

                // "headless" workaround currently "{BaseTest.DH_TLS_AUTHSERVER_BASE_URL}/connect/authorize" redirects immediately to the callback uri (ie there's no UI)
                var response = await AuthServer_Authorise(cookieContainer, allowRedirects) ?? throw new InvalidOperationException("Authorization response was null.");

                // Return authcode and idtoken
                return ExtractAuthCodeIdToken(response);
            }
            finally
            {
                await callback.Stop();
            }

            (string authCode, string idToken) ExtractAuthCodeIdToken(HttpResponseMessage response)
            {
                Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(ExtractAuthCodeIdToken), nameof(DataHolderAuthoriseService));

                string? authCode = null;
                string? idToken = null;

                // Handle Authorisation Code Flow response
                if (ResponseType == ResponseType.Code)
                {
                    var queryValues = HttpUtility.ParseQueryString(response.Headers.Location?.Query ?? throw new InvalidOperationException("Query string parse result was null"));

                    // Check query has "response" param
                    var queryValueResponse = queryValues["response"];
                    var encodedJwt = queryValueResponse;
                    queryValueResponse.Should().NotBeNullOrEmpty();

                    if (AuthServerOptions.JARM_ENCRYPTION_ON)
                    {
                        Log.Information("Authorisation Server has JARM Encryption turned on. JWT is being decrypted.");

                        // Check claims of decode jwt
                        var encryptedJwt = new JwtSecurityTokenHandler().ReadJwtToken(encodedJwt);
                        encryptedJwt.Header["alg"].Should().Be("RSA-OAEP", because: "JARM Encryption is turned on.");
                        encryptedJwt.Header["enc"].Should().Be("A128CBC-HS256", because: "JARM Encryption is turned on.");

                        // Decrypt the JARM JWT.
                        var privateKeyCertificate = new X509Certificate2(JwtCertificateFilename, JwtCertificatePassword, X509KeyStorageFlags.Exportable);
                        var privateKey = privateKeyCertificate.GetRSAPrivateKey();
                        JweToken token = JWE.Decrypt(queryValueResponse, privateKey);
                        encodedJwt = token.Plaintext;
                    }

                    // Return Authorisation Code
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(encodedJwt);
                    authCode = jwt.Claim("code").Value;
                }

                // Handle Hybrid Flow response
                else if (ResponseType == ResponseType.CodeIdToken)
                {
                    var fragment = response.RequestMessage?.RequestUri?.Fragment;
                    if (fragment == null)
                    {
                        throw new Exception($"{nameof(ExtractAuthCodeIdToken)} - response fragment is null").Log();
                    }

                    var query = HttpUtility.ParseQueryString(fragment.TrimStart('#'));

                    authCode = query["code"];
                    idToken = query["id_token"];
                }
                else
                {
                    throw new NotSupportedException($"Only '{ResponseType.Code.ToEnumMemberAttrValue()}' and '{ResponseType.CodeIdToken.ToEnumMemberAttrValue()}' Response types are supported.");
                }

                if (authCode == null)
                {
                    throw new InvalidOperationException("authCode cannot be null.");
                }

                if (idToken == null && ResponseType == ResponseType.CodeIdToken)
                {
                    throw new InvalidOperationException("idToken cannot be null for hybrid flow.");
                }

                return (authCode, idToken) !;
            }
        }

        private async Task<HttpResponseMessage?> AuthServer_Authorise(CookieContainer cookieContainer, bool allowRedirect = true)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(AuthServer_Authorise), nameof(DataHolderAuthoriseService));

            var request = new HttpRequestMessage(HttpMethod.Get, AuthoriseUrl);

#pragma warning disable CS8602 // Dereference of a possibly null reference. This has been disabled because TestAutomationOptions is assigned in the constructor following a null check
            Helpers.AuthServer.AttachHeadersForStandAlone(request.RequestUri?.AbsoluteUri ?? throw new InvalidOperationException($"{nameof(request.RequestUri.AbsoluteUri)} is null").Log(), request.Headers, TestAutomationOptions.DH_MTLS_GATEWAY_URL, AuthServerOptions.XTLSCLIENTCERTTHUMBPRINT, AuthServerOptions.STANDALONE);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var response = await Helpers.Web.CreateHttpClient(allowAutoRedirect: allowRedirect, cookieContainer: cookieContainer).SendAsync(request);

            return response;
        }
        #endregion

        #region Dataholder

        // Call authorise endpoint, should respond with a redirect to auth UI, return the redirect URI
        private async Task<Uri> Authorise_GetRedirectUri()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Authorise_GetRedirectUri), nameof(DataHolderAuthoriseService));

            var queryString = new Dictionary<string, string?>
            {
                { "request_uri", RequestUri },
                { "response_type", ResponseType.ToEnumMemberAttrValue() },
                { "response_mode", ResponseMode.ToEnumMemberAttrValue() },
                { "client_id", ClientId },
                { "redirect_uri", RedirectURI },
                { "scope", Scope },
            };

#pragma warning disable CS8602 // Dereference of a possibly null reference. This is disabled because there is a null check when assigning a value to this in the constructor
            var api = ApiDirector.BuildAuthServerAuthorizeAPI(queryString);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var response = await api.SendAsync(allowAutoRedirect: false);

            if (response.StatusCode != HttpStatusCode.Redirect)
            {
                var content = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                var error = doc.DocumentNode.SelectSingleNode("//input[@name='error']").Attributes["value"].Value;
                var errorDescription = doc.DocumentNode.SelectSingleNode("//input[@name='error_description']").Attributes["value"].Value;

                throw new AuthoriseException($"Expected {HttpStatusCode.Redirect} but got {response.StatusCode}", response.StatusCode, error, errorDescription); // TODO: This was the original code, but it's returning Ok when it should not be 200Ok. Bug 63710
            }

            return response.Headers.Location ?? throw new InvalidOperationException($"{nameof(response.Headers.Location.AbsoluteUri)} is null");
        }

        private async Task<(string authCode, string idToken)> Authorize_Consent(Uri authRedirectUri)
        {
            Log.Information("Calling {FUNCTION} in {ClassName} with Params: {P1}={V1}.", nameof(Authorize_Consent), nameof(DataHolderAuthoriseService), nameof(authRedirectUri), authRedirectUri);

            _ = authRedirectUri.GetLeftPart(UriPartial.Authority) + "/ui";

            string? code = null;
            string? idtoken = null;

            PlaywrightDriver playwrightDriver = new PlaywrightDriver();

            try
            {
                IBrowserContext browserContext = playwrightDriver.NewBrowserContext().Result;

                var page = await browserContext.NewPageAsync();

                await page.GotoAsync(authRedirectUri.AbsoluteUri); // redirect user to Auth UI to login and consent to share accounts

                // Username
                AuthenticateLoginPage authenticateLoginPage = new(page);
                await authenticateLoginPage.EnterCustomerId(UserId ?? throw new InvalidOperationException($"{nameof(UserId)} is null"));
                await authenticateLoginPage.ClickContinue();

                // OTP
                OneTimePasswordPage oneTimePasswordPage = new(page);
                await oneTimePasswordPage.EnterOtp(OTP ?? throw new InvalidOperationException($"{nameof(OTP)} is null"));
                await oneTimePasswordPage.ClickContinue();

                // Select accounts
                SelectAccountsPage selectAccountsPage = new(page);

                var selectedAccountDisplayNames = GetSelectedAccountDisplayNames();

                await selectAccountsPage.SelectAccounts(selectedAccountDisplayNames);
                await selectAccountsPage.ClickContinue();

                // Confirmation - Click authorise and check callback response
                ConfirmAccountSharingPage confirmAccountSharingPage = new(page);

                (code, idtoken) = await HandleDataRecipientCallback(redirectUri: RedirectURI, page: page, setup: async (page) =>
                    {
                        await confirmAccountSharingPage.ClickAuthorise();
                    });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await playwrightDriver.DisposeAsync();
            }

            return (
                authCode: code ?? throw new InvalidOperationException($"{nameof(code)} is null"),
                idToken: idtoken ?? throw new InvalidOperationException($"{nameof(idtoken)} is null"));
        }

        private delegate Task HandleDataRecipientCallback_Setup(IPage page);

        private async Task<(string code, string idtoken)> HandleDataRecipientCallback(string redirectUri, IPage page, HandleDataRecipientCallback_Setup setup)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(HandleDataRecipientCallback), nameof(DataHolderAuthoriseService));

            var callback = new DataRecipientConsentCallback(redirectUri);
            callback.Start();
            try
            {
                await setup(page);

                var callbackRequest = await callback.WaitForCallback();

                if (ResponseMode == ResponseMode.Jwt)
                {
                    callbackRequest.Should().NotBeNull();
                    callbackRequest?.received.Should().BeTrue();
                    var queryValues = HttpUtility.ParseQueryString(callbackRequest?.queryString ?? throw new InvalidOperationException($"{nameof(callbackRequest.queryString)} is null"));
                    var encodedJwt = queryValues["response"];
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(encodedJwt);
                    return (jwt.Claim("code").Value, string.Empty);
                }
                else
                {
                    throw new NotSupportedException(ResponseMode.ToEnumMemberAttrValue() + " response mode is not supported.");
                }                
            }
            finally
            {
                await callback.Stop();
            }
        }
        #endregion

        private string[] GetSelectedAccountDisplayNames()
        {
            if (SelectedAccountDisplayNamesCache != null)
            {
                return SelectedAccountDisplayNamesCache;
            }

            List<string> list = new();

            if (SelectedAccountIdsArray != null)
            {
                using var connection = new SqlConnection(TestAutomationOptions.DATAHOLDER_CONNECTIONSTRING);
                foreach (var accountId in SelectedAccountIdsArray)
                {
                    var displayName = connection.QuerySingle<string>("select displayName from account where accountId = @AccountId", new { AccountId = accountId });
                    list.Add(displayName);
                }
            }

            SelectedAccountDisplayNamesCache = list.ToArray();

            return SelectedAccountDisplayNamesCache;
        }
    }
}
