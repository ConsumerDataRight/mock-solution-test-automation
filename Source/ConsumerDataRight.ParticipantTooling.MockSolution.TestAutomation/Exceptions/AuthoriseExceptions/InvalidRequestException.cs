using System.Net;
using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidRequestException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidRequestException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_request", detail)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRequestException"/> class.
        /// <para>Status code is variable to handle for any non-400 cases like: 302 (Redirect).</para>
        /// </summary>
        public InvalidRequestException(string detail, HttpStatusCode statusCode)
         : base(string.Empty, statusCode, "invalid_request", detail)
        { }

        protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class UnsupportedRequestUriFormParameterException : InvalidRequestException
    {
        public UnsupportedRequestUriFormParameterException() : base(Constants.ErrorMessages.Par.ParRequestUriFormParameterNotSupported)
        { }

        protected UnsupportedRequestUriFormParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class UnsupportedResponseModeException : InvalidRequestException
    {
        public UnsupportedResponseModeException() : base(Constants.ErrorMessages.General.UnsupportedResponseMode)
        { }

        protected UnsupportedResponseModeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class UnsupportedResponseTypeException : InvalidRequestException
    {
        public UnsupportedResponseTypeException() : base(Constants.ErrorMessages.General.UnsupportedResponseType)
        { }

        protected UnsupportedResponseTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingResponseTypeException : InvalidRequestException
    {
        public MissingResponseTypeException() : base(Constants.ErrorMessages.General.MissingResponseType)
        { }

        protected MissingResponseTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingOpenIdScopeException : InvalidRequestException
    {
        public MissingOpenIdScopeException() : base("OpenID Connect requests MUST contain the openid scope value.") //TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected MissingOpenIdScopeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingAuthorizationCodeException : InvalidRequestException
    {
        public MissingAuthorizationCodeException() : base(Constants.ErrorMessages.General.MissingCode)
        { }

        protected MissingAuthorizationCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidRedirectUriForClientException : InvalidRequestException
    {
        public InvalidRedirectUriForClientException() : base(Constants.ErrorMessages.General.InvalidRedirectUri)
        { }

        protected InvalidRedirectUriForClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    #region 302Redirects

    [Serializable]
    public class UnsupportedResponseTypeRedirectException : InvalidRequestException
    {
        public UnsupportedResponseTypeRedirectException() : base("Unsupported response_type", HttpStatusCode.Redirect)//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected UnsupportedResponseTypeRedirectException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class InvalidClientIdRedirectException : InvalidRequestException
    {
        public InvalidClientIdRedirectException() : base("Invalid client ID.", HttpStatusCode.Redirect)//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected InvalidClientIdRedirectException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
    #endregion
}
