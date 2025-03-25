namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public class MissingRequiredHeaderException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRequiredHeaderException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public MissingRequiredHeaderException(string detail, string message)
            : base(CdsError.MissingRequiredHeader, detail, System.Net.HttpStatusCode.BadRequest, message)
        {
        }

        public MissingRequiredHeaderException(string detail)
          : base(CdsError.MissingRequiredHeader, detail, System.Net.HttpStatusCode.BadRequest, null)
        {
        }
    }
}
