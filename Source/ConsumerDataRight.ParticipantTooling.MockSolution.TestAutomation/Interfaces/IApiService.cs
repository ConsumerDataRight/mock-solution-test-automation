namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using System.Net.Http.Headers;

    public interface IApiService
    {
        string? Accept { get; }

        string? AccessToken { get; }

        string? CertificateFilename { get; }

        string? CertificatePassword { get; }

        HttpContent? Content { get; }

        MediaTypeHeaderValue? ContentType { get; }

        IEnumerable<string>? Cookies { get; }

        string? DhMtlsGatewayUrl { get; }

        HttpMethod? HttpMethod { get; }

        string? IfNoneMatch { get; }

        bool IsStandalone { get; }

        string? URL { get; }

        string? XFapiAuthDate { get; }

        string? XFapiInteractionId { get; }

        string? XMinV { get; }

        string? XtlsClientCertificateThumbprint { get; }

        string? XV { get; }

        Task<HttpResponseMessage> SendAsync(bool allowAutoRedirect = true, string? xtlsThumbprint = null);
    }
}