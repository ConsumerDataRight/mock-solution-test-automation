using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    public class InvalidFieldException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFieldException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidFieldException(string detail, string message)
            : base(CdsError.InvalidField, detail, System.Net.HttpStatusCode.BadRequest, message)
        { }

        public InvalidFieldException(string detail)
         : base(CdsError.InvalidField, detail, System.Net.HttpStatusCode.BadRequest, null)
        { }
    }
}
