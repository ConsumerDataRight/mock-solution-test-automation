using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IDataHolderAuthoriseService
    {
        string? CdrArrangementId { get; }
        string CertificateFilename { get; }
        string CertificatePassword { get; }
        string ClientId { get; }
        string JwtCertificateFilename { get; }
        string JwtCertificatePassword { get; }
        string OTP { get; }
        string RedirectURI { get; }
        string? RequestUri { get; }
        ResponseMode ResponseMode { get; }
        ResponseType ResponseType { get; }
        string Scope { get; }
        string? SelectedAccountIds { get; }
        int? SharingDuration { get; }
        int TokenLifetime { get; }
        string UserId { get; }

        Task<(string authCode, string idToken)> Authorise();
        Task<HttpResponseMessage> AuthoriseForJarm();
    }
}