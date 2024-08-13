namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    public class InvalidRequestObjectException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestObjectException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidRequestObjectException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_request_object", detail)
        { }

    }

    public class TokenValidationRequestException : InvalidRequestObjectException
    {
        public TokenValidationRequestException() : base(Constants.ErrorMessages.Jwt.JwtValidationErro.Replace("{0}","request"))
        { }
    }

    public class ExpiredRequestException : InvalidRequestObjectException
    {
        public ExpiredRequestException() : base(Constants.ErrorMessages.Jwt.JwtExpired.Replace("{0}", "request"))
        { }
    }

    public class InvalidExpClaimException : InvalidRequestObjectException
    {
        public InvalidExpClaimException() : base(Constants.ErrorMessages.General.ExpiryGreaterThan60AfterNbf)
        { }
    }

    public class InvalidResponseModeForResponseTypeException : InvalidRequestObjectException
    {
        public InvalidResponseModeForResponseTypeException() : base(Constants.ErrorMessages.General.InvalidResponseModeForResponseType)
        { }
    }

    public class InvalidJwtException : InvalidRequestObjectException
    {
        public InvalidJwtException() : base("Invalid JWT request")//TODO: Should a error gen code and add to constants. Bug 64146
        { }
    }

    public class InvalidArrangementIdException : InvalidRequestObjectException
    {
        public InvalidArrangementIdException() : base(Constants.ErrorMessages.General.InvalidCdrArrangementId)
        { }
    }

    public class MissingNbfClaimException : InvalidRequestObjectException
    {
        public MissingNbfClaimException() : base(Constants.ErrorMessages.General.MissingNbf)
        { }
    }
}
