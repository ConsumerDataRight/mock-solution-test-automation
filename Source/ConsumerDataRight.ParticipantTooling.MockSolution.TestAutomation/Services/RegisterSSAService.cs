using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public class RegisterSsaService : IRegisterSsaService
    {
        private readonly TestAutomationOptions _options;
        private readonly TestAutomationAuthServerOptions _authServerOptions;
        private readonly IApiServiceDirector _apiServiceDirector;

        public RegisterSsaService(IOptions<TestAutomationOptions> options, IOptions<TestAutomationAuthServerOptions> authServerOptions, IApiServiceDirector apiServiceDirector)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _authServerOptions = authServerOptions.Value ?? throw new ArgumentNullException(nameof(authServerOptions));
            _apiServiceDirector = apiServiceDirector ?? throw new ArgumentNullException(nameof(apiServiceDirector));
        }
        /// <summary>
        /// Get SSA from the Register
        /// </summary>
        public async Task<string> GetSSA(
            string brandId,
            string softwareProductId,
            string xv = "3",
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword,
            Industry? industry = null)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(GetSSA), nameof(RegisterSsaService));

            if (industry == null)
            {
                industry = _options.INDUSTRY switch
                {
                    Industry.BANKING => Industry.BANKING,
                    Industry.ENERGY => Industry.ALL, //Energy was using the ALL parameter
                    _ => throw new ArgumentException($"{nameof(_options.INDUSTRY)}")
                };
            }

            // Get access token 
            var registerAccessToken = await new AccessTokenService(_options.DH_MTLS_AUTHSERVER_TOKEN_URL)
            {
                URL = _options.REGISTER_MTLS_TOKEN_URL,
                CertificateFilename = Constants.Certificates.CertificateFilename,
                CertificatePassword = Constants.Certificates.CertificatePassword,
                JwtCertificateFilename = jwtCertificateFilename,
                JwtCertificatePassword = jwtCertificatePassword,
                ClientId = softwareProductId,
                Scope = "cdr-register:read",
                ClientAssertionType = Constants.ClientAssertionType,
                GrantType = "client_credentials",
                Issuer = softwareProductId,
                Audience = _options.REGISTER_MTLS_TOKEN_URL
            }.GetAsync(_options.DH_MTLS_GATEWAY_URL, _authServerOptions.XTLSCLIENTCERTTHUMBPRINT, _authServerOptions.STANDALONE);

            // Get the SSA 
            var api = _apiServiceDirector.BuildRegisterSSAAPI(industry, brandId, softwareProductId, registerAccessToken, xv);
            var response = await api.SendAsync();

            var ssa = await response.Content.ReadAsStringAsync();

            return ssa;
        }
    }
}