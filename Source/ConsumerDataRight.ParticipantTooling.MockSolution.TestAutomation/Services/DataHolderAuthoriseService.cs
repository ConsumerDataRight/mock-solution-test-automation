using System.Net;
using System.Security;
using System.Web;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.APIs;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation;
using Dapper;
using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.Playwright;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public partial class DataHolderAuthoriseService : IDataHolderAuthoriseService
    {

        private DataHolderAuthoriseService() { } //private constructor ensures that the builder must be used to instantiate a new service

        private string[]? SelectedAccountDisplayNamesCache { get; set; } = null;
        private string? AuthoriseUrl { get; set; }
        private TestAutomationOptions TestAutomationOptions { get; set; }
        private TestAutomationAuthServerOptions? AuthServerOptions { get; set; }

        private IApiServiceDirector? ApiDirector { get; set; }

        /// <summary>
        /// The customer's userid with the DataHolder - eg "jwilson"
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// The OTP (One-time password) that is sent to the customer (via sms etc) so the DataHolder can authenticate the Customer.
        /// For the mock solution use "000789"
        /// </summary>
        public string OTP { get; private set; } = Constants.AuthoriseOTP;

        ///// <summary>
        ///// Comma delimited list of account ids the user is granting consent for
        ///// </summary>
        public string? SelectedAccountIds { get; private set; }

        protected string[]? SelectedAccountIdsArray => SelectedAccountIds?.Split(",");

        /// <summary>
        /// Scope
        /// </summary>
        public string Scope { get; private set; }

        /// <summary>
        /// Lifetime (in seconds) of the access token
        /// </summary>
        public int TokenLifetime { get; private set; } = Constants.AuthServer.DefaultTokenLifetime;

        /// <summary>
        /// Lifetime (in seconds) of the CDR arrangement.
        /// SHARING_DURATION = 90 days
        /// </summary>
        public int? SharingDuration { get; private set; } = Constants.AuthServer.SharingDuration;

        public string? RequestUri { get; private set; }

        public string CertificateFilename { get; private set; }
        public string CertificatePassword { get; private set; }
        public string ClientId { get; private set; }
        public string RedirectURI { get; private set; }
        public string JwtCertificateFilename { get; private set; }
        public string JwtCertificatePassword { get; private set; }

        public ResponseType ResponseType { get; private set; }
        public ResponseMode ResponseMode { get; private set; } = ResponseMode.Fragment;

        public string? CdrArrangementId { get; private set; }

        public class DataHolderAuthoriseServiceBuilder : IAsyncBuilder<DataHolderAuthoriseService>
        {
            public DataHolderAuthoriseServiceBuilder(TestAutomationOptions options, IDataHolderParService dataHolderParService, IApiServiceDirector apiServiceDirector, bool useAdditionalDefaults = false, TestAutomationAuthServerOptions? authServerOptions = null)
            {
                _useAdditionalDefaults = useAdditionalDefaults;
                _dataHolderParService = dataHolderParService ?? throw new ArgumentNullException(nameof(dataHolderParService));

                _dataHolderAuthoriseService.TestAutomationOptions = options ?? throw new ArgumentNullException(nameof(options));
                _dataHolderAuthoriseService.ApiDirector = apiServiceDirector ?? throw new ArgumentNullException(nameof(apiServiceDirector));
                _dataHolderAuthoriseService.AuthServerOptions = authServerOptions; //no null check here because this is nullable
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

                var _options = _dataHolderAuthoriseService.TestAutomationOptions; //purely to shorten reference

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
                               responseMode: _dataHolderAuthoriseService.ResponseMode
                           );
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
                           responseMode: _dataHolderAuthoriseService.ResponseMode,
                           cdrArrangementId: _dataHolderAuthoriseService.CdrArrangementId
                           );
                    }
                }

                return _dataHolderAuthoriseService;
            }

            private void FillMissingDefaults()
            {
                Log.Information("Filling missing defaults for public properties");

                var _options = _dataHolderAuthoriseService.TestAutomationOptions; //purely to shorten reference

                //Anything that doesn't have a value which should...will get it's default value set
                if (_dataHolderAuthoriseService.ClientId.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.ClientId = _options.LastRegisteredClientId ?? throw new InvalidOperationException($"{nameof(_options.LastRegisteredClientId)} should not be null.").Log();
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.ClientId, nameof(ClientId));
                }

                if (_dataHolderAuthoriseService.RedirectURI.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.RedirectURI = _useAdditionalDefaults ? _options.ADDITIONAL_SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS : _options.SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS;
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.RedirectURI, nameof(RedirectURI));
                }

                if (_dataHolderAuthoriseService.Scope.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.Scope = _options.SCOPE;
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.Scope, nameof(Scope));
                }

                if (_dataHolderAuthoriseService.SelectedAccountIds.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.SelectedAccountIds = GetAccountIdsForUser(_dataHolderAuthoriseService.UserId);
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.SelectedAccountIds, nameof(SelectedAccountIds));
                }

                if (_dataHolderAuthoriseService.CertificateFilename.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.CertificateFilename = _useAdditionalDefaults ? Constants.Certificates.AdditionalCertificateFilename : Constants.Certificates.CertificateFilename;
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.CertificateFilename, nameof(CertificateFilename));
                }

                if (_dataHolderAuthoriseService.CertificatePassword.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.CertificatePassword = _useAdditionalDefaults ? Constants.Certificates.AdditionalCertificatePassword : Constants.Certificates.CertificatePassword;
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.CertificatePassword, nameof(CertificatePassword));
                }

                if (_dataHolderAuthoriseService.JwtCertificateFilename.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.JwtCertificateFilename = _useAdditionalDefaults ? Constants.Certificates.AdditionalJwksCertificateFilename : Constants.Certificates.JwtCertificateFilename;
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.JwtCertificateFilename, nameof(JwtCertificateFilename));
                }

                if (_dataHolderAuthoriseService.JwtCertificatePassword.IsNullOrWhiteSpace())
                {
                    _dataHolderAuthoriseService.JwtCertificatePassword = _useAdditionalDefaults ? Constants.Certificates.AdditionalJwksCertificatePassword : Constants.Certificates.JwtCertificatePassword;
                    Log.Information("Assigned default value {VALUE} to Parameter {PARAM}", _dataHolderAuthoriseService.JwtCertificatePassword, nameof(JwtCertificatePassword));
                }
            }

            private void Validate()
            {
                Log.Information("Validating {BUILTOBJECT} mandatory properties.", nameof(DataHolderAuthoriseService));
                if (_dataHolderAuthoriseService.RedirectURI.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException(nameof(RedirectURI)).Log();
                }

                if (_dataHolderAuthoriseService.ClientId.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException(nameof(ClientId)).Log();
                }

                if (_dataHolderAuthoriseService.UserId.IsNullOrWhiteSpace())
                {
                    throw new ArgumentNullException(nameof(UserId)).Log();
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
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(Authorise), nameof(DataHolderAuthoriseService));

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
                var responseMode = Enums.ResponseMode.FormPost.ToEnumMemberAttrValue(); //to ensure it's clearly consistent

                Uri authRedirectUri = await Authorise_GetRedirectUri(responseMode);

                return await Authorize_Consent(authRedirectUri, responseMode);
            }
        }

        #region AuthServer
        public async Task<HttpResponseMessage> AuthoriseForJarm()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(AuthoriseForJarm), nameof(DataHolderAuthoriseService));

            var callback = new DataRecipientConsentCallback(RedirectURI);
            callback.Start();
            try
            {
                var cookieContainer = new CookieContainer();
                var response = await AuthServer_Authorise(cookieContainer, false) ?? throw new NullReferenceException();
                return response;
            }
            finally
            {
                await callback.Stop();
            }
        }

        /// <summary>
        /// Perform authorisation and consent flow. Returns authCode and idToken
        /// </summary>
        private async Task<(string authCode, string idToken)> AuthoriseHeadless()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(AuthoriseHeadless), nameof(DataHolderAuthoriseService));

            var callback = new DataRecipientConsentCallback(RedirectURI);
            callback.Start();
            try
            {
                var cookieContainer = new CookieContainer();

                // "headless" workaround currently "{BaseTest.DH_TLS_AUTHSERVER_BASE_URL}/connect/authorize" redirects immediately to the callback uri (ie there's no UI)
                var response = await AuthServer_Authorise(cookieContainer) ?? throw new NullReferenceException();

                // Return authcode and idtoken
                return ExtractAuthCodeIdToken(response);
            }
            finally
            {
                await callback.Stop();
            }

            static (string authCode, string idToken) ExtractAuthCodeIdToken(HttpResponseMessage response)
            {
                Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(ExtractAuthCodeIdToken), nameof(DataHolderAuthoriseService));

                var fragment = response.RequestMessage?.RequestUri?.Fragment;
                if (fragment == null)
                {
                    throw new Exception($"{nameof(ExtractAuthCodeIdToken)} - response fragment is null").Log();
                }

                var query = HttpUtility.ParseQueryString(fragment.TrimStart('#'));

                Exception RaiseException(string errorMessage, string? authCode, string? idToken)
                {
                    var responseRequestUri = response?.RequestMessage?.RequestUri;
                    return new SecurityException($"{errorMessage}\r\nauthCode={authCode},idToken={idToken},response.RequestMessage.RequestUri={responseRequestUri}");
                }

                string? authCode = query["code"];
                string? idToken = query["id_token"];

                if (authCode == null)
                {
                    throw RaiseException("authCode is null", authCode, idToken);
                }

                if (idToken == null)
                {
                    throw RaiseException("idToken is null", authCode, idToken);
                }

                return (authCode, idToken);
            }
        }

        private async Task<HttpResponseMessage?> AuthServer_Authorise(CookieContainer cookieContainer, bool allowRedirect = true)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(AuthServer_Authorise), nameof(DataHolderAuthoriseService));

            var request = new HttpRequestMessage(HttpMethod.Get, AuthoriseUrl);

#pragma warning disable CS8602 // Dereference of a possibly null reference. This has been disabled because TestAutomationOptions is assigned in the constructor following a null check
            Helpers.AuthServer.AttachHeadersForStandAlone(request.RequestUri?.AbsoluteUri ?? throw new NullReferenceException($"{nameof(request.RequestUri.AbsoluteUri)} is null").Log(), request.Headers, TestAutomationOptions.DH_MTLS_GATEWAY_URL, AuthServerOptions.XTLSCLIENTCERTTHUMBPRINT, AuthServerOptions.STANDALONE);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var response = await Helpers.Web.CreateHttpClient(allowAutoRedirect: allowRedirect, cookieContainer: cookieContainer).SendAsync(request);

            return response;
        }
        #endregion

        #region Dataholder
        // Call authorise endpoint, should respond with a redirect to auth UI, return the redirect URI
        private async Task<Uri> Authorise_GetRedirectUri(string responseMode)
        {
            Log.Information("Calling {FUNCTION} in {ClassName} with Params: {P1}={V1}.", nameof(Authorise_GetRedirectUri), nameof(DataHolderAuthoriseService), nameof(responseMode), responseMode);

            var queryString = new Dictionary<string, string?>
            {
                { "request_uri", RequestUri },
                { "response_type", ResponseType.CodeIdToken.ToEnumMemberAttrValue() },
                { "response_mode", responseMode },
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

                throw new AuthoriseException($"Expected {HttpStatusCode.Redirect} but got {response.StatusCode}", response.StatusCode, error, errorDescription); //TODO: This was the original code, but it's returning Ok when it should not be 200Ok. Bug 63710
            }

            return response.Headers.Location ?? throw new NullReferenceException(nameof(response.Headers.Location.AbsoluteUri));
        }


        private async Task<(string authCode, string idToken)> Authorize_Consent(Uri authRedirectUri, string responseMode)
        {
            Log.Information("Calling {FUNCTION} in {ClassName} with Params: {P1}={V1},{P2}={V2}.", nameof(Authorize_Consent), nameof(DataHolderAuthoriseService), nameof(authRedirectUri), authRedirectUri, nameof(responseMode), responseMode);

            var authRedirectLeftPart = authRedirectUri.GetLeftPart(UriPartial.Authority) + "/ui";

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
                await authenticateLoginPage.EnterCustomerId(UserId ?? throw new NullReferenceException(nameof(UserId)));
                await authenticateLoginPage.ClickContinue();

                // OTP
                OneTimePasswordPage oneTimePasswordPage = new(page);
                await oneTimePasswordPage.EnterOtp(OTP ?? throw new NullReferenceException(nameof(OTP)));
                await oneTimePasswordPage.ClickContinue();

                // Select accounts
                SelectAccountsPage selectAccountsPage = new(page);

                var selectedAccountDisplayNames = GetSelectedAccountDisplayNames();

                await selectAccountsPage.SelectAccounts(selectedAccountDisplayNames);
                await selectAccountsPage.ClickContinue();

                // Confirmation - Click authorise and check callback response
                ConfirmAccountSharingPage confirmAccountSharingPage = new(page);

                (code, idtoken) = await HybridFlow_HandleCallback(redirectUri: RedirectURI, responseMode: responseMode, page: page, setup: async (page) =>
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
                authCode: code ?? throw new NullReferenceException(nameof(code)),
                idToken: idtoken ?? throw new NullReferenceException(nameof(idtoken))
            );
        }

        private delegate Task HybridFlow_HandleCallback_Setup(IPage page);
        static private async Task<(string code, string idtoken)> HybridFlow_HandleCallback(string redirectUri, string responseMode, IPage page, HybridFlow_HandleCallback_Setup setup)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(HybridFlow_HandleCallback), nameof(DataHolderAuthoriseService));

            var callback = new DataRecipientConsentCallback(redirectUri);
            callback.Start();
            try
            {
                await setup(page);

                var callbackRequest = await callback.WaitForCallback();
                switch (responseMode)
                {
                    case "form_post":
                        {
                            callbackRequest.Should().NotBeNull();
                            callbackRequest?.received.Should().BeTrue();
                            callbackRequest?.method.Should().Be(HttpMethod.Post);
                            callbackRequest?.body.Should().NotBeNullOrEmpty();

                            var body = QueryHelpers.ParseQuery(callbackRequest?.body);
                            var code = body["code"];
                            var id_token = body["id_token"];
                            return (code, id_token);
                        }
                    case "fragment":
                    case "query":
                    default:
                        throw new NotSupportedException(nameof(responseMode));
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
