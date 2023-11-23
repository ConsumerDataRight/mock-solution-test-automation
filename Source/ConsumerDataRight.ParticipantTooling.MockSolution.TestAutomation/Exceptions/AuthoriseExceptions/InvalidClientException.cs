using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidClientException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidClientException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidClientException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_client", detail)
        { }

        protected InvalidClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class ClientNotFoundException : InvalidClientException
    {
        public ClientNotFoundException() : base(Constants.ErrorMessages.General.ClientNotFound)
        { }

        protected ClientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingClientAssertionException : InvalidClientException
    {
        public MissingClientAssertionException() : base(Constants.ErrorMessages.General.ClientAssertionNotProvided)
        { }

        protected MissingClientAssertionException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingClientAssertionTypeException : InvalidClientException
    {
        public MissingClientAssertionTypeException() : base(Constants.ErrorMessages.ClientAssertion.ClientAssertionTypeNotProvided)
        { }

        protected MissingClientAssertionTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidClientAssertionTypeException : InvalidClientException
    {
        public InvalidClientAssertionTypeException() : base(Constants.ErrorMessages.ClientAssertion.InvalidClientAssertionType)
        { }

        protected InvalidClientAssertionTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidClientAssertionFormatException : InvalidClientException
    {
        public InvalidClientAssertionFormatException() : base(Constants.ErrorMessages.ClientAssertion.ClientAssertionInvalidFormat)
        { }

        protected InvalidClientAssertionFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingIssClaimException : InvalidClientException
    {
        public MissingIssClaimException() : base(Constants.ErrorMessages.ClientAssertion.ClientAssertionMissingIssClaim)
        { }

        protected MissingIssClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingJtiException : InvalidClientException
    {
        public MissingJtiException() : base(Constants.ErrorMessages.General.JtiRequired)
        { }

        protected MissingJtiException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class TokenValidationClientAssertionException : InvalidClientException
    {
        public TokenValidationClientAssertionException() : base(Constants.ErrorMessages.Jwt.JwtValidationErro.Replace("{0}","client_assertion"))
        { }

        protected TokenValidationClientAssertionException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidAudienceException : InvalidClientException
    {
        public InvalidAudienceException() : base(Constants.ErrorMessages.Jwt.JwtInvalidAudience.Replace("{0}", "client_assertion"))
        { }

        protected InvalidAudienceException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class ExpiredClientAssertionException : InvalidClientException
    {
        public ExpiredClientAssertionException() : base(Constants.ErrorMessages.Jwt.JwtExpired.Replace("{0}", "client_assertion"))
        { }

        protected ExpiredClientAssertionException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
