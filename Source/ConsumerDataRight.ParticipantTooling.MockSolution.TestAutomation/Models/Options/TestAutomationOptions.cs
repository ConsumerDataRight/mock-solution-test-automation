namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("SonarAnalyzer", "CA1707:Remove the underscores from member name", Justification = "Renaming these now require change in other repos as well.")]
    public class TestAutomationOptions
    {
        public bool IS_AUTH_SERVER { get; set; } = false;

        public Industry INDUSTRY { get; set; }

        public string SCOPE { get; set; }

        public string DH_MTLS_GATEWAY_URL { get; set; }

        public string DH_MTLS_AUTHSERVER_TOKEN_URL => DH_MTLS_GATEWAY_URL + "/idp/connect/token";

        public string DH_TLS_AUTHSERVER_BASE_URL { get; set; }

        public string DH_TLS_PUBLIC_BASE_URL { get; set; }

        public string REGISTER_MTLS_URL { get; set; }

        public string REGISTER_MTLS_TOKEN_URL => REGISTER_MTLS_URL + "/idp/connect/token";

        public string REGISTRATION_AUDIENCE_URI => DH_TLS_AUTHSERVER_BASE_URL;

        // Migrated from Auth Server Settings as needed for DH tests
        public string CDRAUTHSERVER_SECUREBASEURI { get; set; }

        // Connection strings
        public string DATAHOLDER_CONNECTIONSTRING { get; set; }

        public string AUTHSERVER_CONNECTIONSTRING { get; set; }

        public string REGISTER_CONNECTIONSTRING { get; set; }

        // Seed-data offset
        public bool SEEDDATA_OFFSETDATES { get; set; }

        public string MDH_INTEGRATION_TESTS_HOST { get; set; }

        public string SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS => $"{MDH_INTEGRATION_TESTS_HOST}:9999/consent/callback";

        public string SOFTWAREPRODUCT_JWKS_URI_FOR_INTEGRATION_TESTS => $"{MDH_INTEGRATION_TESTS_HOST}:9998/jwks";

        public string MDH_HOST { get; set; }

        public string ADDITIONAL_SOFTWAREPRODUCT_REDIRECT_URI_FOR_INTEGRATION_TESTS => $"{MDH_INTEGRATION_TESTS_HOST}:9997/consent/callback";

        public string ADDITIONAL_SOFTWAREPRODUCT_JWKS_URI_FOR_INTEGRATION_TESTS => $"{MDH_INTEGRATION_TESTS_HOST}:9996/jwks";

        // For Playwright testing
        public bool RUNNING_IN_CONTAINER { get; set; } = false;

        public bool CREATE_MEDIA { get; set; } = false;

        public int TEST_TIMEOUT { get; set; } = 30000;

        public string MEDIA_FOLDER { get; set; }

        // Store dynamic client id for each successful Data Holder registration request
        public string? LastRegisteredClientId { get; set; }
    }
}
