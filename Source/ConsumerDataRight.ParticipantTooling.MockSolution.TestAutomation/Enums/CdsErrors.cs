namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models;

    public enum CdsError
    {
        [CdrError("Expected Error Encountered", "urn:au-cds:error:cds-all:GeneralError/Expected")]
        ExpectedError,

        [CdrError("Unexpected Error Encountered", "urn:au-cds:error:cds-all:GeneralError/Unexpected")]
        UnexpectedError,

        [CdrError("Service Unavailable", "urn:au-cds:error:cds-all:Service/Unavailable")]
        ServiceUnavailable,

        [CdrError("Missing Required Field", "urn:au-cds:error:cds-all:Field/Missing")]
        MissingRequiredField,

        [CdrError("Missing Required Header", "urn:au-cds:error:cds-all:Header/Missing")]
        MissingRequiredHeader,

        [CdrError("Invalid Field", "urn:au-cds:error:cds-all:Field/Invalid")]
        InvalidField,

        [CdrError("Invalid Header", "urn:au-cds:error:cds-all:Header/Invalid")]
        InvalidHeader,

        [CdrError("Invalid Date", "urn:au-cds:error:cds-all:Field/InvalidDateTime")]
        InvalidDate,

        [CdrError("Invalid Page Size", "urn:au-cds:error:cds-all:Field/InvalidPageSize")]
        InvalidPageSize,

        [CdrError("Invalid Version", "urn:au-cds:error:cds-all:Header/InvalidVersion")]
        InvalidVersion,

        [CdrError("ADR Status Is Not Active", "urn:au-cds:error:cds-all:Authorisation/AdrStatusNotActive")]
        ADRStatusIsNotActive,

        [CdrError("Consent Is Revoked", "urn:au-cds:error:cds-all:Authorisation/RevokedConsent")]
        ConsentIsRevoked,

        [CdrError("Consent Is Invalid", "urn:au-cds:error:cds-all:Authorisation/InvalidConsent")]
        ConsentIsInvalid,

        [CdrError("Resource Not Implemented", "urn:au-cds:error:cds-all:Resource/NotImplemented")]
        ResourceNotImplemented,

        [CdrError("Resource Not Found", "urn:au-cds:error:cds-all:Resource/NotFound")]
        ResourceNotFound,

        [CdrError("Unsupported Version", "urn:au-cds:error:cds-all:Header/UnsupportedVersion")]
        UnsupportedVersion,

        [CdrError("Invalid Consent Arrangement", "urn:au-cds:error:cds-all:Authorisation/InvalidArrangement")]
        InvalidConsentArrangement,

        [CdrError("Invalid Page", "urn:au-cds:error:cds-all:Field/InvalidPage")]
        InvalidPage,

        [CdrError("Invalid Resource", "urn:au-cds:error:cds-all:Resource/Invalid")]
        InvalidResource,

        [CdrError("Unavailable Resource", "urn:au-cds:error:cds-all:Resource/Unavailable")]
        UnavailableResource,

        [CdrError("Invalid Brand", "urn:au-cds:error:cds-register:Field/InvalidBrand")]
        InvalidBrand,

        [CdrError("Invalid Industry", "urn:au-cds:error:cds-register:Field/InvalidIndustry")]
        InvalidIndustry,

        [CdrError("Invalid Software Product", "urn:au-cds:error:cds-register:Field/InvalidSoftwareProduct")]
        InvalidSoftwareProduct,

        [CdrError("Invalid Energy Account", "urn:au-cds:error:cds-energy:Authorisation/InvalidEnergyAccount")]
        InvalidEnergyAccount,

        [CdrError("Invalid Banking Account", "urn:au-cds:error:cds-banking:Authorisation/InvalidBankingAccount")]
        InvalidBankingAccount,

        [CdrError("Unavailable Banking Account", "urn:au-cds:error:cds-banking:Authorisation/UnavailableBankingAccount")]
        UnavailableBankingAccount,
    }
}
