namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IDataHolderTokenRevocationService
    {
        Task<HttpResponseMessage> SendRequest(string? clientId = null, string? clientAssertionType = Constants.ClientAssertionType, string? clientAssertion = null, string? token = null, string? tokenTypeHint = null, string? certificateFilename = Constants.Certificates.CertificateFilename, string? certificatePassword = Constants.Certificates.CertificatePassword, string? jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename, string? jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword);
    }
}