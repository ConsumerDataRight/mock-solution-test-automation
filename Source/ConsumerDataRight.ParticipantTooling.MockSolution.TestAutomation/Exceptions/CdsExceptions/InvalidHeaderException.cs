namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public class InvalidHeaderException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHeaderException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidHeaderException(string detail, string message)
            : base(CdsError.InvalidHeader, detail, System.Net.HttpStatusCode.BadRequest, message)
        {
        }

        public InvalidHeaderException(string detail)
          : base(CdsError.InvalidHeader, detail, System.Net.HttpStatusCode.BadRequest, null)
        {
        }
    }
}
