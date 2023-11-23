using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidSoftwareStatementException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSoftwareStatementException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidSoftwareStatementException()
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_software_statement", Constants.ErrorMessages.Dcr.SsaValidationFailed)
        { }

        protected InvalidSoftwareStatementException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
