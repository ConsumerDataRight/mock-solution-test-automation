namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using System.Net;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public class InvalidConsentException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsentException"/> class.
        /// <para>Status code: 403 (Forbidden).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidConsentException(string detail, string message)
            : base(CdsError.ConsentIsInvalid, detail, HttpStatusCode.Forbidden, message)
        {
        }

        public InvalidConsentException(string detail)
        : base(CdsError.ConsentIsInvalid, detail, HttpStatusCode.Forbidden, null)
        {
        }

        public InvalidConsentException()
        : base(CdsError.ConsentIsInvalid, "The authorised consumer's consent is insufficient to execute the resource", HttpStatusCode.Forbidden, null)
        {
        }
    }
}
