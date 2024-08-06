namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    public class InvalidClientException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidClientException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidClientException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_client", detail)
        { }
    }

    public class ClientNotFoundException : InvalidClientException
    {
        public ClientNotFoundException() : base(Constants.ErrorMessages.General.ClientNotFound)
        { }
    }

    public class MissingClientAssertionException : InvalidClientException
    {
        public MissingClientAssertionException() : base(Constants.ErrorMessages.General.ClientAssertionNotProvided)
        { }
    }

    public class MissingClientAssertionTypeException : InvalidClientException
    {
        public MissingClientAssertionTypeException() : base(Constants.ErrorMessages.ClientAssertion.ClientAssertionTypeNotProvided)
        { }
    }

    public class InvalidClientAssertionTypeException : InvalidClientException
    {
        public InvalidClientAssertionTypeException() : base(Constants.ErrorMessages.ClientAssertion.InvalidClientAssertionType)
        { }
    }

    public class InvalidClientAssertionFormatException : InvalidClientException
    {
        public InvalidClientAssertionFormatException() : base(Constants.ErrorMessages.ClientAssertion.ClientAssertionInvalidFormat)
        { }
    }

    public class MissingIssClaimException : InvalidClientException
    {
        public MissingIssClaimException() : base(Constants.ErrorMessages.ClientAssertion.ClientAssertionMissingIssClaim)
        { }
    }

    public class MissingJtiException : InvalidClientException
    {
        public MissingJtiException() : base(Constants.ErrorMessages.General.JtiRequired)
        { }
    }

    public class TokenValidationClientAssertionException : InvalidClientException
    {
        public TokenValidationClientAssertionException() : base(Constants.ErrorMessages.Jwt.JwtValidationErro.Replace("{0}","client_assertion"))
        { }
    }

    public class InvalidAudienceException : InvalidClientException
    {
        public InvalidAudienceException() : base(Constants.ErrorMessages.Jwt.JwtInvalidAudience.Replace("{0}", "client_assertion"))
        { }
    }

    public class ExpiredClientAssertionException : InvalidClientException
    {
        public ExpiredClientAssertionException() : base(Constants.ErrorMessages.Jwt.JwtExpired.Replace("{0}", "client_assertion"))
        { }
    }
}
