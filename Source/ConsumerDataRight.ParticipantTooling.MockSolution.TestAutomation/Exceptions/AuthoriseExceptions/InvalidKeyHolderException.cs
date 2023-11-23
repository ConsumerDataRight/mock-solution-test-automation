using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class InvalidKeyHolderException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeyHolderException"/> class.
        /// <para>Status code: 401 (Unauthorized).</para>
        /// </summary>
        public InvalidKeyHolderException()
          : base(string.Empty, System.Net.HttpStatusCode.Unauthorized, "invalid_token", Constants.ErrorMessages.Authorization.AuthorizationHolderOfKeyCheckFailed)
        { }

        protected InvalidKeyHolderException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
