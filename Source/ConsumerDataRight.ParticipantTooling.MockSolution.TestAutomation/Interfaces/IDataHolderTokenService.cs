using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IDataHolderTokenService
    {
        Task<string?> GetAccessToken(string authCode);
        Task<TokenResponse?> GetResponse(string authCode, int? shareDuration = null, string? clientId = null, string? redirectUri = null, string certificateFilename = Constants.Certificates.CertificateFilename, string certificatePassword = Constants.Certificates.CertificatePassword, string jwkCertificateFilename = Constants.Certificates.JwtCertificateFilename, string jwkCertificatePassword = Constants.Certificates.JwtCertificatePassword, string? scope = null);
        Task<HttpResponseMessage> SendRequest(string? authCode = null, bool usePut = false, string grantType = "authorization_code", string? clientId = null, string? issuerClaim = null, string clientAssertionType = Constants.ClientAssertionType, bool useClientAssertion = true, int? shareDuration = null, string? refreshToken = null, string? customClientAssertion = null, string? scope = null, string? redirectUri = null, string certificateFilename = Constants.Certificates.CertificateFilename, string certificatePassword = Constants.Certificates.CertificatePassword, string jwkCertificateFilename = Constants.Certificates.JwtCertificateFilename, string jwkCertificatePassword = Constants.Certificates.JwtCertificatePassword);
        Task<TokenResponse?> DeserializeResponse(HttpResponseMessage response);
        Task<TokenResponse?> GetResponseUsingRefreshToken(string? refreshToken, string? scope = null);
    }
}