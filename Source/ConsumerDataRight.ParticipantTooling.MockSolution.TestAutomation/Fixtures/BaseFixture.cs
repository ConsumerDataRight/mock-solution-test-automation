namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Fixtures
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
    using Dapper;
    using Dapper.Contrib.Extensions;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Serilog;
    using Xunit;

    /// <summary>
    /// Patches Register SoftwareProduct RedirectURI and JwksURI.
    /// Stands up JWKS endpoint.
    /// </summary>
    public class BaseFixture : IAsyncLifetime
    {
        private JwksEndpoint? _jwksEndpoint;
        private readonly TestAutomationOptions _options;

        public BaseFixture(IOptions<TestAutomationOptions> options)
        {
            Log.Information("Constructing {ClassName}.", nameof(BaseFixture));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public ServiceProvider ServiceProvider { get; }

        public Task InitializeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(InitializeAsync), nameof(BaseFixture));

            // Patch Register
            Helpers.AuthServer.PatchRedirectUriForRegister(_options);
            Helpers.AuthServer.PatchJwksUriForRegister(_options);
            if (_options.INDUSTRY.Equals(Industry.ENERGY))
            {
                Register_PatchScopes();
            }

            // Stand-up JWKS endpoint
            _jwksEndpoint = new JwksEndpoint(
                _options.SOFTWAREPRODUCT_JWKS_URI_FOR_INTEGRATION_TESTS,
                Constants.Certificates.JwtCertificateFilename,
                Constants.Certificates.JwtCertificatePassword);
            _jwksEndpoint.Start();

            // The Auth Server testing seeds specific data
            if (_options.IS_AUTH_SERVER)
            {
                CdrAuthServer_SeedDatabase();
            }

            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(DisposeAsync), nameof(BaseFixture));

            if (_jwksEndpoint != null)
            {
                await _jwksEndpoint.DisposeAsync();
            }
        }

        /// <summary>
        /// The seed data for the Register is using the loopback uri for jwksuri.
        /// Since the integration tests stands up it's own data recipient jwks endpoint we need to
        /// patch the jwks uri to match our endpoint.
        /// </summary>
        private void Register_PatchScopes()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Register_PatchScopes), nameof(BaseFixture));
            using var connection = new SqlConnection(_options.REGISTER_CONNECTIONSTRING);
            connection.Open();

            using var updateCommand = new SqlCommand("update softwareproduct set scope = 'openid profile bank:accounts.basic:read bank:accounts.detail:read bank:transactions:read bank:payees:read bank:regular_payments:read common:customer.basic:read common:customer.detail:read cdr:registration energy:accounts.basic:read energy:accounts.concessions:read' where lower(softwareproductid) = @id", connection);
            updateCommand.Parameters.AddWithValue("@id", Constants.SoftwareProducts.SoftwareProductId.ToLower());
            updateCommand.ExecuteNonQuery();
        }

        private void CdrAuthServer_SeedDatabase()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(CdrAuthServer_SeedDatabase), nameof(BaseFixture));
            using var connection = new SqlConnection(_options.AUTHSERVER_CONNECTIONSTRING);

            connection.Query("delete softwareproducts");
            if (connection.QuerySingle<int>("select count(*) from softwareproducts") != 0)
            {
                throw new InvalidOperationException("Unable to delete softwareproducts");
            }

            connection.Insert(new SoftwareProduct()
            {
                SoftwareProductId = Constants.SoftwareProducts.SoftwareProductId,
                SoftwareProductName = "Mock Data Recipient Software Product",
                SoftwareProductDescription = "Mock Data Recipient Software Product",
                LogoUri = "https://cdrsandbox.gov.au/logo192.png",
                Status = "ACTIVE",
                LegalEntityId = "18B75A76-5821-4C9E-B465-4709291CF0F4",
                LegalEntityName = "Mock Data Recipient Legal Entity Name",
                LegalEntityStatus = "ACTIVE",
                BrandId = Constants.Brands.BrandId,
                BrandName = "Mock Data Recipient Brand Name",
                BrandStatus = "ACTIVE",
            });

            connection.Insert(new SoftwareProduct()
            {
                SoftwareProductId = Constants.SoftwareProducts.AdditionalSoftwareProductId,
                SoftwareProductName = "Track Xpense",
                SoftwareProductDescription = "Application to allow you to track your expenses",
                LogoUri = "https://cdrsandbox.gov.au/foo.png",
                Status = "ACTIVE",
                LegalEntityId = "9d34ede4-2c76-4ecc-a31e-ea8392d31cc9",
                LegalEntityName = "FintechX",
                LegalEntityStatus = "ACTIVE",
                BrandId = Constants.Brands.AdditionalBrandId,
                BrandName = "Finance X",
                BrandStatus = "ACTIVE",
            });
        }

        private class SoftwareProduct
        {
            public string? SoftwareProductId { get; set; }

            public string? SoftwareProductName { get; set; }

            public string? SoftwareProductDescription { get; set; }

            public string? LogoUri { get; set; }

            public string? Status { get; set; }

            public string? LegalEntityId { get; set; }

            public string? LegalEntityName { get; set; }

            public string? LegalEntityStatus { get; set; }

            public string? BrandId { get; set; }

            public string? BrandName { get; set; }

            public string? BrandStatus { get; set; }
        }
    }
}
