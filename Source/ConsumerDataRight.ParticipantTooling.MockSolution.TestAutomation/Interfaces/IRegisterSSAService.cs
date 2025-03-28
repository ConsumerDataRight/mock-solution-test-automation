﻿namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public interface IRegisterSsaService
    {
        Task<string> GetSSA(string brandId, string softwareProductId, string xv = "3", string jwtCertificateFilename = Constants.Certificates.JwtCertificateFilename, string jwtCertificatePassword = Constants.Certificates.JwtCertificatePassword, Industry? industry = null);
    }
}