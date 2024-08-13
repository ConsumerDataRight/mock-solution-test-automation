using System.Net;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
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

    }

    public class UnsupportedRequestUriFormParameterException : InvalidRequestException
    {
        public UnsupportedRequestUriFormParameterException() : base(Constants.ErrorMessages.Par.ParRequestUriFormParameterNotSupported)
        { }
    }

    public class UnsupportedResponseModeException : InvalidRequestException
    {
        public UnsupportedResponseModeException() : base(Constants.ErrorMessages.General.UnsupportedResponseMode)
        { }
    }

    public class UnsupportedResponseTypeException : InvalidRequestException
    {
        public UnsupportedResponseTypeException() : base(Constants.ErrorMessages.General.UnsupportedResponseType)
        { }
    }

    public class MissingResponseTypeException : InvalidRequestException
    {
        public MissingResponseTypeException() : base(Constants.ErrorMessages.General.MissingResponseType)
        { }
    }

    public class MissingOpenIdScopeException : InvalidRequestException
    {
        public MissingOpenIdScopeException() : base("OpenID Connect requests MUST contain the openid scope value.") //TODO: Should a error gen code and add to constants. Bug 64146
        { }
    }

    public class MissingAuthorizationCodeException : InvalidRequestException
    {
        public MissingAuthorizationCodeException() : base(Constants.ErrorMessages.General.MissingCode)
        { }
    }

    public class InvalidRedirectUriForClientException : InvalidRequestException
    {
        public InvalidRedirectUriForClientException() : base(Constants.ErrorMessages.General.InvalidRedirectUri)
        { }

    }

    #region 302Redirects

    public class UnsupportedResponseTypeRedirectException : InvalidRequestException
    {
        public UnsupportedResponseTypeRedirectException() : base("Unsupported response_type", HttpStatusCode.Redirect)//TODO: Should a error gen code and add to constants. Bug 64146
        { }
    }

    public class InvalidClientIdRedirectException : InvalidRequestException
    {
        public InvalidClientIdRedirectException() : base("Invalid client ID.", HttpStatusCode.Redirect)//TODO: Should a error gen code and add to constants. Bug 64146
        { }
    }
    #endregion
}
