namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services;

    public interface IApiServiceDirector
    {
        ApiService BuildAuthServerAuthorizeAPI(Dictionary<string, string?> queryString);

        ApiService BuildAuthServerJWKSAPI();

        ApiService BuildAuthServerOpenIdConfigurationAPI();

        ApiService BuildCustomerResourceAPI(string? accessToken);

        ApiService BuildDataHolderDiscoveryStatusAPI();

        ApiService BuildDataHolderDiscoveryOutagesAPI();

        ApiService BuildDataHolderBankingGetAccountsAPI(string? accessToken, string? xFapiAuthDate, string? xv = "2", string? xFapiInteractionId = null, string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null);

        ApiService BuildDataHolderBankingGetTransactionsAPI(string? accessToken, string? xFapiAuthDate, string? encryptedAccountId = null, string? xv = "1", string? xFapiInteractionId = null, string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null);

        ApiService BuildDataHolderCommonGetCustomerAPI(string? accessToken, string? xFapiAuthDate, string? xv = "1", string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword);

        ApiService BuildDataholderRegisterAPI(string? accessToken, string? registrationRequest, HttpMethod? httpMethod, string clientId = "");

        ApiService BuildRegisterSSAAPI(Industry? industry, string brandId, string softwareProductId, string? accessToken, string? xv);

        ApiService BuildUserInfoAPI(string? xv, string? accessToken, string? thumbprint, HttpMethod? httpMethod, string certFilename = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword);

        ApiService BuildDataHolderEnergyGetAccountsAPI(string? accessToken, string? xFapiAuthDate, string? xv = "1", string? xMinV = null, string? xFapiInteractionId = null, string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null);

        ApiService BuildDataHolderEnergyGetConcessionsAPI(string? accessToken, string? xFapiAuthDate, string? encryptedAccountId = null, string? xv = "1", string certFileName = Constants.Certificates.CertificateFilename, string certPassword = Constants.Certificates.CertificatePassword, string? url = null);
    }
}
