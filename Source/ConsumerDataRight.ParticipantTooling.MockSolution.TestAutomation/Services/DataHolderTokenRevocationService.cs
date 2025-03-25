namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    using System.Security.Cryptography.X509Certificates;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
    using Microsoft.Extensions.Options;

    public class DataHolderTokenRevocationService : IDataHolderTokenRevocationService
    {
        private readonly TestAutomationOptions _options;
        private readonly TestAutomationAuthServerOptions? _authServerOptions;

        public DataHolderTokenRevocationService(IOptions<TestAutomationOptions> options, IOptions<TestAutomationAuthServerOptions>? authServerOptions = null)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _authServerOptions = authServerOptions?.Value; // no null check here because this is nullable for Data Holder projects
        }

        // Send token request, returning HttpResponseMessage
        public async Task<HttpResponseMessage> SendRequest(
            string? clientId = null,
            string clientAssertionType = Constants.ClientAssertionType,
            string? clientAssertion = null,
            string? token = null,
            string? tokenTypeHint = null,
            string certificateFilename = Constants.Certificates.CertificateFilename,
            string certificatePassword = Constants.Certificates.CertificatePassword,
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword)
        {
            clientId ??= _options.LastRegisteredClientId;

            var url = $"{_options.DH_MTLS_GATEWAY_URL}/connect/revocation";
            var formFields = GenerateFormFields(clientId, clientAssertionType, clientAssertion, token, tokenTypeHint, jwtCertificateFilename, jwtCertificatePassword, url);

            using var clientHandler = CreateHttpClientHandler(certificateFilename, certificatePassword);
            using var client = new HttpClient(clientHandler);

            var content = new FormUrlEncodedContent(formFields);
            Helpers.AuthServer.AttachHeadersForStandAlone(url, content.Headers, _options.DH_MTLS_GATEWAY_URL, _authServerOptions?.XTLSCLIENTCERTTHUMBPRINT, _authServerOptions?.STANDALONE);

            var responseMessage = await client.PostAsync(url, content);
            return responseMessage;
        }

        private static List<KeyValuePair<string?, string?>> GenerateFormFields(
            string? clientId,
            string clientAssertionType,
            string? clientAssertion,
            string? token,
            string? tokenTypeHint,
            string jwtCertificateFilename,
            string jwtCertificatePassword,
            string url)
        {
            var formFields = new List<KeyValuePair<string?, string?>>();

            if (clientId != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("client_id", clientId.ToLower()));
            }

            if (clientAssertionType != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("client_assertion_type", clientAssertionType));
            }

            formFields.Add(new KeyValuePair<string?, string?>("client_assertion", clientAssertion ?? GenerateClientAssertion(jwtCertificateFilename, jwtCertificatePassword, clientId, url)));

            if (token != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("token", token));
            }

            if (tokenTypeHint != null)
            {
                formFields.Add(new KeyValuePair<string?, string?>("token_type_hint", tokenTypeHint));
            }

            return formFields;
        }

        private static string GenerateClientAssertion(string jwtCertificateFilename, string jwtCertificatePassword, string? clientId, string url)
        {
            return new PrivateKeyJwtService
            {
                CertificateFilename = jwtCertificateFilename,
                CertificatePassword = jwtCertificatePassword,
                Issuer = clientId ?? throw new InvalidOperationException($"{nameof(clientId)} can not be null.").Log(),
                Audience = url,
            }.Generate();
        }

        private static HttpClientHandler CreateHttpClientHandler(string certificateFilename, string certificatePassword)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            clientHandler.ClientCertificates.Add(new X509Certificate2(
                certificateFilename ?? throw new ArgumentNullException(nameof(certificateFilename)),
                certificatePassword,
                X509KeyStorageFlags.Exportable));

            return clientHandler;
        }
    }
}
