namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IPrivateKeyJwtService
    {
        string Audience { get; set; }

        string CertificateFilename { get; set; }

        string CertificatePassword { get; set; }

        string Issuer { get; set; }

        bool RequireIssuer { get; init; }

        string Generate();
    }
}