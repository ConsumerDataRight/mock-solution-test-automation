using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IDataHolderRegisterService
    {
        string CreateRegistrationRequest(string ssa, string tokenEndpointAuthSigningAlg = "PS256", string[]? redirectUris = null, string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword, string applicationType = "web", string requestObjectSigningAlg = "PS256", string responseType = "code id_token", string[]? grantTypes = null, string? authorizationSignedResponseAlg = null, string? authorizationEncryptedResponseAlg = null, string? authorizationEncryptedResponseEnc = null, string? idTokenSignedResponseAlg = "PS256", string? idTokenEncryptedResponseAlg = "RSA-OAEP", string? idTokenEncryptedResponseEnc = "A256GCM");
        Task<HttpResponseMessage> RegisterSoftwareProduct(string registrationRequest);
        Task<(string ssa, string registration, string clientId)> RegisterSoftwareProduct(string brandId = Constants.Brands.BrandId, string softwareProductId = Constants.SoftwareProducts.SoftwareProductId, string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword, string responseType = "code id_token", string authorizationSignedResponseAlg = "PS256");
    }
}