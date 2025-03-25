namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    public class InvalidTokenException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTokenException"/> class.
        /// <para>Status code: 401 (Unauthorized).</para>
        /// </summary>
        public InvalidTokenException()
          : base("401", "Unauthorized", "invalid_token", System.Net.HttpStatusCode.Unauthorized, null)
        {
        }
    }
}
