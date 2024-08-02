using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    public class InvalidVersionException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVersionException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidVersionException(string detail, string message)
            : base(CdsError.InvalidVersion, detail, System.Net.HttpStatusCode.BadRequest, message)
        { }

        public InvalidVersionException(string detail)
         : base(CdsError.InvalidVersion, detail, System.Net.HttpStatusCode.BadRequest, null)
        { }

        public InvalidVersionException()
        : base(CdsError.InvalidVersion, "Version is not a positive Integer.", System.Net.HttpStatusCode.BadRequest, null)
        { }
    }
}
