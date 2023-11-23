using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class ExpiredRequestUriException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpiredRequestUriException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public ExpiredRequestUriException()
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_request_uri", Constants.ErrorMessages.Authorization.ExpiredRequestUri)
        {
        }

        protected ExpiredRequestUriException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
