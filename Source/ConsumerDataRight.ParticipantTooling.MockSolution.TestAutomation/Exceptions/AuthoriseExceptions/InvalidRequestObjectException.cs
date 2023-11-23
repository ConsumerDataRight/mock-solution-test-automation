using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidRequestObjectException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestObjectException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidRequestObjectException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_request_object", detail)
        { }

        protected InvalidRequestObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class TokenValidationRequestException : InvalidRequestObjectException
    {
        public TokenValidationRequestException() : base(Constants.ErrorMessages.Jwt.JwtValidationErro.Replace("{0}","request"))
        { }

        protected TokenValidationRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class ExpiredRequestException : InvalidRequestObjectException
    {
        public ExpiredRequestException() : base(Constants.ErrorMessages.Jwt.JwtExpired.Replace("{0}", "request"))
        { }

        protected ExpiredRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidExpClaimException : InvalidRequestObjectException
    {
        public InvalidExpClaimException() : base(Constants.ErrorMessages.General.ExpiryGreaterThan60AfterNbf)
        { }

        protected InvalidExpClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidResponseModeForResponseTypeException : InvalidRequestObjectException
    {
        public InvalidResponseModeForResponseTypeException() : base(Constants.ErrorMessages.General.InvalidResponseModeForResponseType)
        { }

        protected InvalidResponseModeForResponseTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidJwtException : InvalidRequestObjectException
    {
        public InvalidJwtException() : base("Invalid JWT request")//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected InvalidJwtException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidArrangementIdException : InvalidRequestObjectException
    {
        public InvalidArrangementIdException() : base(Constants.ErrorMessages.General.InvalidCdrArrangementId)
        { }

        protected InvalidArrangementIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingNbfClaimException : InvalidRequestObjectException
    {
        public MissingNbfClaimException() : base(Constants.ErrorMessages.General.MissingNbf)
        { }

        protected MissingNbfClaimException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
