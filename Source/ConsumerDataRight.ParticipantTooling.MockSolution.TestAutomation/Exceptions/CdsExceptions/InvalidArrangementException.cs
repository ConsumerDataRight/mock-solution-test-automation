using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    [Serializable]
    public class InvalidArrangementException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArrangementException"/> class.
        /// <para>Status code: 422 (Unprocessable Entity).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidArrangementException(string detail, string message)
            : base(CdsError.InvalidConsentArrangement, detail, System.Net.HttpStatusCode.UnprocessableEntity, message)
        { }

        public InvalidArrangementException(string detail)
          : base(CdsError.InvalidConsentArrangement, detail, System.Net.HttpStatusCode.UnprocessableEntity, null)
        { }

        protected InvalidArrangementException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
