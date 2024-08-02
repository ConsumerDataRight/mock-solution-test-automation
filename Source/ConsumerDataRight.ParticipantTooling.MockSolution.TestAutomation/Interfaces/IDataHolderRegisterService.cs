using Microsoft.IdentityModel.Tokens;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IDataHolderRegisterService
    {
        string CreateRegistrationRequest(string ssa, 
            string tokenEndpointAuthSigningAlg = SecurityAlgorithms.RsaSsaPssSha256, 
            string[]? redirectUris = null, 
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename, 
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword, 
            string applicationType = "web", 
            string requestObjectSigningAlg = SecurityAlgorithms.RsaSsaPssSha256, 
            string responseType = "code id_token", 
            string[]? grantTypes = null, 
            string? authorizationSignedResponseAlg = "PS256", 
            string? authorizationEncryptedResponseAlg = null, 
            string? authorizationEncryptedResponseEnc = null, 
            string? idTokenSignedResponseAlg = "PS256", 
            string? idTokenEncryptedResponseAlg = "RSA-OAEP", 
            string? idTokenEncryptedResponseEnc = "A256GCM");

        Task<HttpResponseMessage> RegisterSoftwareProduct(string registrationRequest);

        Task<(string ssa, string registration, string clientId)> RegisterSoftwareProduct(string brandId = Constants.Brands.BrandId, string softwareProductId = Constants.SoftwareProducts.SoftwareProductId, string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword, string responseType = "code,code id_token", string authorizationSignedResponseAlg = SecurityAlgorithms.RsaSsaPssSha256);
    }
}