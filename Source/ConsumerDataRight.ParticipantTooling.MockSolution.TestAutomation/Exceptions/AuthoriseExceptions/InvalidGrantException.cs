namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    public class InvalidGrantException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidGrantException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidGrantException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_grant", detail)
        { }

    }

    public class ExpiredAuthorizationCodeException : InvalidGrantException
    {
        public ExpiredAuthorizationCodeException() : base(Constants.ErrorMessages.Token.AuthorizationCodeExpired)
        { }

    }

    public class InvalidAuthorizationCodeException : InvalidGrantException
    {
        public InvalidAuthorizationCodeException() : base(Constants.ErrorMessages.Token.InvalidAuthorizationCode)
        { }

    }

    public class MissingRefreshTokenException : InvalidGrantException
    {
        public MissingRefreshTokenException() : base(Constants.ErrorMessages.Token.MissingRefreshToken)
        { }

    }

    public class InvalidRefreshTokenException : InvalidGrantException
    {
        public InvalidRefreshTokenException() : base(Constants.ErrorMessages.Token.InvalidRefreshToken)
        { }

    }
}
