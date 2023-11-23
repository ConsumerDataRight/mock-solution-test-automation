using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTestAutomationSettings(this IServiceCollection services, Action<TestAutomationOptions> configureTestAutomationOptions)
        {
            Log.Information($"Configuring Test Automation settings.");

            services.Configure(configureTestAutomationOptions);

            var options = new TestAutomationOptions();
            configureTestAutomationOptions(options);

            try
            {
                //Doesn't check for Industry because it's an enum and defaults to banking if not supplied
                if (options.SCOPE.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.SCOPE)} - required setting is missing from StartUp");
                }

                if (options.DH_MTLS_GATEWAY_URL.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.DH_MTLS_GATEWAY_URL)} - configuration setting not found");
                }

                if (options.DH_TLS_AUTHSERVER_BASE_URL.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.DH_TLS_AUTHSERVER_BASE_URL)} - configuration setting not found");
                }

                if (options.DH_TLS_PUBLIC_BASE_URL.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.DH_TLS_PUBLIC_BASE_URL)} - configuration setting not found");
                }

                if (options.REGISTER_MTLS_URL.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.REGISTER_MTLS_URL)} - configuration setting not found");
                }

                if (options.DATAHOLDER_CONNECTIONSTRING.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.DATAHOLDER_CONNECTIONSTRING)} - configuration setting not found");
                }

                if (options.AUTHSERVER_CONNECTIONSTRING.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.AUTHSERVER_CONNECTIONSTRING)} - configuration setting not found");
                }

                if (options.REGISTER_CONNECTIONSTRING.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.REGISTER_CONNECTIONSTRING)} - configuration setting not found");
                }

                if (options.MDH_INTEGRATION_TESTS_HOST.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.MDH_INTEGRATION_TESTS_HOST)} - configuration setting not found");
                }

                if (options.MDH_HOST.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.MDH_HOST)} - configuration setting not found");
                }

                if (options.CDRAUTHSERVER_SECUREBASEURI.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.CDRAUTHSERVER_SECUREBASEURI)} - configuration setting not found");
                }

                if (options.CREATE_MEDIA && options.MEDIA_FOLDER.IsNullOrWhiteSpace())
                {
                    throw new InvalidOperationException($"{nameof(TestAutomationOptions.MEDIA_FOLDER)} - configuration setting not found and must be provided when {nameof(TestAutomationOptions.CREATE_MEDIA)} is true");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }

            return services;
        }

        public static IServiceCollection AddTestAutomationAuthServerSettings(this IServiceCollection services, Action<TestAutomationAuthServerOptions> configureTestAutomationAuthServerOptions)
        {
            services.Configure(configureTestAutomationAuthServerOptions);

            var options = new TestAutomationAuthServerOptions();
            configureTestAutomationAuthServerOptions(options);

            if (options.XTLSCLIENTCERTTHUMBPRINT.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException($"{nameof(TestAutomationAuthServerOptions.XTLSCLIENTCERTTHUMBPRINT)} - configuration setting not found").Log();
            }

            if (options.XTLSADDITIONALCLIENTCERTTHUMBPRINT.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException($"{nameof(TestAutomationAuthServerOptions.XTLSADDITIONALCLIENTCERTTHUMBPRINT)} - configuration setting not found").Log();
            }

            if (options.CDRAUTHSERVER_BASEURI.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException($"{nameof(TestAutomationAuthServerOptions.CDRAUTHSERVER_BASEURI)} - configuration setting not found").Log();
            }

            if (options.ACCESSTOKENLIFETIMESECONDS == null)
            {
                throw new InvalidOperationException($"{nameof(TestAutomationAuthServerOptions.ACCESSTOKENLIFETIMESECONDS)} - configuration setting not found").Log();
            }

            return services;
        }

        public static IServiceCollection AddTestAutomationServices(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Information($"Registering Test Automation shared services.");

            if (configuration == null) 
            {
                throw new InvalidOperationException($"{nameof(AddTestAutomationServices)} - configuration cannot be null").Log();
            }

            services.AddSingleton<ISqlQueryService, SqlQueryService>();
            services.AddSingleton<IDataHolderParService, DataHolderParService>();
            services.AddSingleton<IDataHolderTokenService, DataHolderTokenService>();
            services.AddSingleton<IDataHolderRegisterService, DataHolderRegisterService>();
            services.AddSingleton<IRegisterSsaService, RegisterSsaService>();
            services.AddSingleton<IDataHolderAccessTokenCache, DataHolderAccessTokenCache>();
            services.AddSingleton<IApiServiceDirector, ApiServiceDirector>();
            services.AddSingleton<IDataHolderTokenRevocationService, DataHolderTokenRevocationService>();

            services.AddSingleton(configuration);

            services.AddSingleton(configuration);

            return services;
        }
    }
}
