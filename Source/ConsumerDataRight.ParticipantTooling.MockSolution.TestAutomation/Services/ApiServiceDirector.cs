using System.Net.Http.Headers;
using System.Text;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Serilog;
using static ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services.ApiService;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public class ApiServiceDirector : IApiServiceDirector
    {
        private readonly TestAutomationOptions _options;
        private readonly TestAutomationAuthServerOptions _authServerOptions;
        private ApiServiceBuilder _builder;

        public ApiServiceDirector(IOptions<TestAutomationOptions> options, IOptions<TestAutomationAuthServerOptions> authServerOptions)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _authServerOptions = authServerOptions.Value ?? throw new ArgumentNullException(nameof(authServerOptions));
        }

        public ApiService BuildUserInfoAPI(string? xv, string? accessToken, string? thumbprint, HttpMethod? httpMethod, string certFilename = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildUserInfoAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                .WithHttpMethod(HttpMethod.Get)
                .WithUrl($"{_options.DH_MTLS_GATEWAY_URL}/connect/userinfo")
                .WithXV(xv)
                .WithXFapiAuthDate(DateTime.Now.ToUniversalTime().ToString("r"))
                .WithAccessToken(accessToken)
                .WithHttpMethod(httpMethod)
                .WithCertificateFilename(certFilename)
                .WithCertificatePassword(certPassword)
                .WithIsStandalone(_authServerOptions.STANDALONE)
                .WithDhMtlsGatewayUrl(_options.DH_MTLS_GATEWAY_URL)
                .WithXtlsClientCertificateThumbprint(thumbprint)
                .Build();
        }

        public ApiService BuildAuthServerAuthorizeAPI(Dictionary<string, string?> queryString)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildAuthServerAuthorizeAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                .WithHttpMethod(HttpMethod.Get)
                .WithUrl(QueryHelpers.AddQueryString($"{_options.DH_TLS_AUTHSERVER_BASE_URL}/connect/authorize", queryString))
                .Build();
        }

        public ApiService BuildDataholderRegisterAPI(string? accessToken, string? registrationRequest, HttpMethod? httpMethod, string clientId = "")
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataholderRegisterAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            var additionalUrl = clientId.IsNullOrWhiteSpace() ? "" : $"/{clientId}";

            _builder
                .WithUrl($"{_options.DH_MTLS_GATEWAY_URL}/connect/register{additionalUrl}")
                .WithHttpMethod(httpMethod)
                .WithAccessToken(accessToken)
                .WithIsStandalone(_authServerOptions.STANDALONE)
                .WithDhMtlsGatewayUrl(_options.DH_MTLS_GATEWAY_URL)
                .WithXtlsClientCertificateThumbprint(_authServerOptions.XTLSCLIENTCERTTHUMBPRINT);

            if (registrationRequest != null)
            {
                _builder
                .WithContent(new StringContent(registrationRequest, Encoding.UTF8, "application/jwt"))
                .WithContentType(MediaTypeHeaderValue.Parse("application/jwt"))
                .WithAccept("application/json");
            }

            return _builder.Build();
        }

        public ApiService BuildRegisterSSAAPI(Industry? industry, string brandId, string softwareProductId, string? accessToken, string? xv)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildRegisterSSAAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                 .WithUrl($"{_options.REGISTER_MTLS_URL}/cdr-register/v1/{industry}/data-recipients/brands/{brandId}/software-products/{softwareProductId}/ssa")
                .WithHttpMethod(HttpMethod.Get)
                .WithAccessToken(accessToken)
                .WithXV(xv)
                .WithIsStandalone(_authServerOptions.STANDALONE)
                .WithDhMtlsGatewayUrl(_options.DH_MTLS_GATEWAY_URL)
                .WithXtlsClientCertificateThumbprint(_authServerOptions.XTLSCLIENTCERTTHUMBPRINT)
                .Build();
        }
        public ApiService BuildAuthServerOpenIdConfigurationAPI()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildAuthServerOpenIdConfigurationAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                .WithUrl($"{_authServerOptions.CDRAUTHSERVER_BASEURI}/.well-known/openid-configuration")
                .WithHttpMethod(HttpMethod.Get)
                .WithIsStandalone(_authServerOptions.STANDALONE)
                .WithDhMtlsGatewayUrl(_options.DH_MTLS_GATEWAY_URL)
                .WithXtlsClientCertificateThumbprint(_authServerOptions.XTLSCLIENTCERTTHUMBPRINT)
                .Build();
        }
        public ApiService BuildCustomerResourceAPI(string? accessToken)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildCustomerResourceAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                  .WithUrl($"{_options.DH_MTLS_GATEWAY_URL}/resource/cds-au/v1/common/customer")
                .WithHttpMethod(HttpMethod.Get)
                .WithXV("1")
                .WithXFapiAuthDate(DateTime.Now.ToUniversalTime().ToString("r"))
                .WithAccessToken(accessToken)
                .WithIsStandalone(_authServerOptions.STANDALONE)
                .WithDhMtlsGatewayUrl(_options.DH_MTLS_GATEWAY_URL)
                .WithXtlsClientCertificateThumbprint(_authServerOptions.XTLSCLIENTCERTTHUMBPRINT)
                .Build();
        }

        public ApiService BuildAuthServerJWKSAPI()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildAuthServerJWKSAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                .WithUrl($"{_authServerOptions.CDRAUTHSERVER_BASEURI}/.well-known/openid-configuration/jwks")
                .WithHttpMethod(HttpMethod.Get)
                .WithIsStandalone(_authServerOptions.STANDALONE)
                .WithDhMtlsGatewayUrl(_options.DH_MTLS_GATEWAY_URL)
                .WithXtlsClientCertificateThumbprint(_authServerOptions.XTLSCLIENTCERTTHUMBPRINT)
                .Build();
        }

        public ApiService BuildDataHolderDiscoveryStatusAPI()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderDiscoveryStatusAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                 .WithHttpMethod(HttpMethod.Get)
                 .WithXV("1")
                 .WithUrl($"{_options.DH_TLS_PUBLIC_BASE_URL}/cds-au/v1/discovery/status")
                .Build();
        }

        public ApiService BuildDataHolderDiscoveryOutagesAPI()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderDiscoveryOutagesAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                .WithHttpMethod(HttpMethod.Get)
                .WithXV("1")
                .WithUrl($"{_options.DH_TLS_PUBLIC_BASE_URL}/cds-au/v1/discovery/outages")
                .Build();
        }

        public ApiService BuildDataHolderCommonGetCustomerAPI(string? accessToken, string? xFapiAuthDate, string? xv = "1", string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderCommonGetCustomerAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            return _builder
                 .WithCertificateFilename(certFileName)
                .WithCertificatePassword(certPassword)
                   .WithHttpMethod(HttpMethod.Get)
                 .WithXV(xv)
                 .WithUrl($"{_options.DH_MTLS_GATEWAY_URL}/cds-au/v1/common/customer")
                 .WithXFapiAuthDate(xFapiAuthDate)
                 .WithAccessToken(accessToken)
                 .Build();
        }
        public ApiService BuildDataHolderBankingGetAccountsAPI(string? accessToken, string? xFapiAuthDate, string? xv = "1", string? xFapiInteractionId = null, string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderBankingGetAccountsAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            _builder = _builder
                .WithCertificateFilename(certFileName)
                .WithCertificatePassword(certPassword)
                 .WithHttpMethod(HttpMethod.Get)
                 .WithXV(xv)
                 .WithUrl(url ?? $"{_options.DH_MTLS_GATEWAY_URL}/cds-au/v1/banking/accounts")
                 .WithXFapiAuthDate(xFapiAuthDate)
                 .WithAccessToken(accessToken);

            if (!xFapiInteractionId.IsNullOrWhiteSpace())
            {
                _builder = _builder.WithXFapiInteractionId(xFapiInteractionId);
            }

            return _builder.Build();
        }

        public ApiService BuildDataHolderBankingGetTransactionsAPI(string? accessToken, string? xFapiAuthDate, string? encryptedAccountId = null, string? xv = "1", string? xFapiInteractionId = null, string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderBankingGetTransactionsAPI), nameof(ApiServiceDirector));

            if (encryptedAccountId != null && url != null)
            {
                throw new ArgumentException($"Error: {nameof(BuildDataHolderBankingGetTransactionsAPI)} - Can't provide both {nameof(encryptedAccountId)} and {nameof(url)}");
            }

            _builder = new ApiServiceBuilder();

            _builder = _builder
                .WithCertificateFilename(certFileName)
                .WithCertificatePassword(certPassword)
                 .WithHttpMethod(HttpMethod.Get)
                 .WithXV(xv)
                 .WithUrl(url ?? $"{_options.DH_MTLS_GATEWAY_URL}/cds-au/v1/banking/accounts/{encryptedAccountId}/transactions")
                 .WithAccessToken(accessToken);

            if (!xFapiAuthDate.IsNullOrWhiteSpace())
            {
                _builder = _builder.WithXFapiAuthDate(xFapiAuthDate);
            }

            if (!xFapiInteractionId.IsNullOrWhiteSpace())
            {
                _builder = _builder.WithXFapiInteractionId(xFapiInteractionId);
            }

            return _builder.Build();
        }

        public ApiService BuildDataHolderEnergyGetAccountsAPI(string? accessToken, string? xFapiAuthDate, string? xv = "1", string? xMinV = null, string? xFapiInteractionId = null, string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderEnergyGetAccountsAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            _builder = _builder
                .WithCertificateFilename(certFileName)
                .WithCertificatePassword(certPassword)
                .WithHttpMethod(HttpMethod.Get)
                .WithXV(xv)
                .WithUrl(url ?? $"{_options.DH_MTLS_GATEWAY_URL}/cds-au/v1/energy/accounts")
                .WithXFapiAuthDate(xFapiAuthDate)
                .WithAccessToken(accessToken);

            if (!xFapiInteractionId.IsNullOrWhiteSpace())
            {
                _builder = _builder.WithXFapiInteractionId(xFapiInteractionId);
            }

            if (!xMinV.IsNullOrWhiteSpace())
            {
                _builder = _builder.WithXMinV(xMinV);
            }

            return _builder.Build();
        }

        public ApiService BuildDataHolderEnergyGetConcessionsAPI(string? accessToken, string? xFapiAuthDate, string? encryptedAccountId = null, string? xv = "1", string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(BuildDataHolderEnergyGetConcessionsAPI), nameof(ApiServiceDirector));

            _builder = new ApiServiceBuilder();

            if (encryptedAccountId != null && url != null)
            {
                throw new ArgumentException($"Error: {nameof(BuildDataHolderEnergyGetConcessionsAPI)} - Can't provide both {nameof(encryptedAccountId)} and {nameof(url)}").Log();
            }

            _builder = _builder
                .WithCertificateFilename(certFileName)
                .WithCertificatePassword(certPassword)
                 .WithHttpMethod(HttpMethod.Get)
                 .WithXV(xv)
                 .WithUrl(url ?? $"{_options.DH_MTLS_GATEWAY_URL}/cds-au/v1/energy/accounts/{encryptedAccountId}/concessions")
                 .WithXFapiAuthDate(xFapiAuthDate)
                 .WithAccessToken(accessToken);

            return _builder.Build();
        }
    }
}
