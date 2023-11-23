using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class InvalidTokenException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTokenException"/> class.
        /// <para>Status code: 401 (Unauthorized).</para>
        /// </summary>
        public InvalidTokenException()
          : base("401", "Unauthorized", "invalid_token", System.Net.HttpStatusCode.Unauthorized, null)
        { }

        protected InvalidTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
