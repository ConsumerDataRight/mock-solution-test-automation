namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IAccessTokenService
    {
        string Audience { get; init; }
        string? CertificateFilename { get; set; }
        string? CertificatePassword { get; set; }
        string ClientAssertionType { get; init; }
        string ClientId { get; init; }
        string GrantType { get; init; }
        string Issuer { get; init; }
        string? JwtCertificateFilename { get; set; }
        string? JwtCertificatePassword { get; set; }
        string Scope { get; init; }
        string URL { get; init; }

        Task<string?> GetAsync(string dhMtlsGatewayUrl, string xtlsClientCertThumbprint, bool isStandalone);
    }
}