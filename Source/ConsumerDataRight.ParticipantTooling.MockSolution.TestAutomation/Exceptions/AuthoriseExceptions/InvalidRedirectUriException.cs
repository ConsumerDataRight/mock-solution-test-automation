using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidRedirectUriException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRedirectUriException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidRedirectUriException(string redirectUri)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_redirect_uri", Constants.ErrorMessages.Dcr.RegistrationRequestInvalidRedirectUri.Replace("{0}",redirectUri))
        { }

        protected InvalidRedirectUriException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
