namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public class InvalidPageSizeException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPageSizeException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidPageSizeException(string detail, string message)
            : base(CdsError.InvalidPageSize, detail, System.Net.HttpStatusCode.BadRequest, message)
        {
        }

        public InvalidPageSizeException(string detail)
            : base(CdsError.InvalidPageSize, detail, System.Net.HttpStatusCode.BadRequest, null)
        {
        }
    }
}
