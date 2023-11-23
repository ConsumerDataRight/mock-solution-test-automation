using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidGrantException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidGrantException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidGrantException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_grant", detail)
        { }

        protected InvalidGrantException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class ExpiredAuthorizationCodeException : InvalidGrantException
    {
        public ExpiredAuthorizationCodeException() : base(Constants.ErrorMessages.Token.AuthorizationCodeExpired)
        { }

        protected ExpiredAuthorizationCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidAuthorizationCodeException : InvalidGrantException
    {
        public InvalidAuthorizationCodeException() : base(Constants.ErrorMessages.Token.InvalidAuthorizationCode)
        { }

        protected InvalidAuthorizationCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingRefreshTokenException : InvalidGrantException
    {
        public MissingRefreshTokenException() : base(Constants.ErrorMessages.Token.MissingRefreshToken)
        { }

        protected MissingRefreshTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidRefreshTokenException : InvalidGrantException
    {
        public InvalidRefreshTokenException() : base(Constants.ErrorMessages.Token.InvalidRefreshToken)
        { }

        protected InvalidRefreshTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
