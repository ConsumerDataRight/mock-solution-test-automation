﻿namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using static ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services.DataHolderParService;

    public interface IDataHolderParService
    {
        Task<string> GetRequestUri(string? scope, string? clientId = null, string jwtCertificateForClientAssertionFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificateForClientAssertionPassword = Constants.Certificates.JwtCertificatePassword, string jwtCertificateForRequestObjectFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificateForRequestObjectPassword = Constants.Certificates.JwtCertificatePassword, string? redirectUri = null, int? sharingDuration = Constants.AuthServer.SharingDuration, string? cdrArrangementId = null, ResponseType responseType = ResponseType.Code, ResponseMode responseMode = ResponseMode.Jwt);

        Task<HttpResponseMessage> SendRequest(string? scope, string? clientId = null, string clientAssertionType = Constants.ClientAssertionType, int? sharingDuration = Constants.AuthServer.SharingDuration, string? aud = null, int nbfOffsetSeconds = 0, int expOffsetSeconds = 0, bool addRequestObject = true, bool addNotBeforeClaim = true, bool addExpiryClaim = true, string? cdrArrangementId = null, string? redirectUri = null, string? clientAssertion = null, string codeVerifier = Constants.AuthServer.FapiPhase2CodeVerifier, string codeChallengeMethod = Constants.AuthServer.FapiPhase2CodeChallengeMethod, string? requestUri = null, ResponseMode? responseMode = ResponseMode.Jwt, string certificateFilename = Constants.Certificates.CertificateFilename, string certificatePassword = Constants.Certificates.CertificatePassword, string jwtCertificateForClientAssertionFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificateForClientAssertionPassword = Constants.Certificates.JwtCertificatePassword, string jwtCertificateForRequestObjectFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificateForRequestObjectPassword = Constants.Certificates.JwtCertificatePassword, ResponseType? responseType = ResponseType.Code, string? state = null);

        Task<Response?> DeserializeResponse(HttpResponseMessage response);
    }
}