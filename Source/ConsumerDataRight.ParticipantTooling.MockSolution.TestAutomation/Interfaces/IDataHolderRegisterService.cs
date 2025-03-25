namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using Microsoft.IdentityModel.Tokens;

    public interface IDataHolderRegisterService
    {
        string CreateRegistrationRequest(
            string ssa,
            string tokenEndpointAuthSigningAlg = SecurityAlgorithms.RsaSsaPssSha256,
            string[]? redirectUris = null,
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword,
            string applicationType = "web",
            string requestObjectSigningAlg = SecurityAlgorithms.RsaSsaPssSha256,
            ResponseType responseType = ResponseType.Code,
            string[]? grantTypes = null,
            string? authorizationSignedResponseAlg = "PS256",
            string? authorizationEncryptedResponseAlg = null,
            string? authorizationEncryptedResponseEnc = null,
            string? idTokenSignedResponseAlg = "PS256");

        Task<HttpResponseMessage> RegisterSoftwareProduct(string registrationRequest);

        Task<(string ssa, string registration, string clientId)> RegisterSoftwareProduct(
            string brandId = Constants.Brands.BrandId,
            string softwareProductId = Constants.SoftwareProducts.SoftwareProductId,
            string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename,
            string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword,
            ResponseType responseType = ResponseType.Code,
            string authorizationSignedResponseAlg = SecurityAlgorithms.RsaSsaPssSha256);
    }
}