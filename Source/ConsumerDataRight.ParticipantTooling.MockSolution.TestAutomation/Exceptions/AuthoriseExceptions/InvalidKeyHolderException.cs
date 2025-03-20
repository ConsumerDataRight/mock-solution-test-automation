namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    public class InvalidKeyHolderException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeyHolderException"/> class.
        /// <para>Status code: 401 (Unauthorized).</para>
        /// </summary>
        public InvalidKeyHolderException()
          : base(string.Empty, System.Net.HttpStatusCode.Unauthorized, "invalid_token", Constants.ErrorMessages.Authorization.AuthorizationHolderOfKeyCheckFailed)
        {
        }
    }
}
