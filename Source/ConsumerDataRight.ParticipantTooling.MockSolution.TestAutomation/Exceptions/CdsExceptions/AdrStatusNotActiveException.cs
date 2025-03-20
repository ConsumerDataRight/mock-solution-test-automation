namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;

    public class AdrStatusNotActiveException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdrStatusNotActiveException"/> class.
        /// <para>Status code: 403 (Forbidden).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public AdrStatusNotActiveException(string detail, string message)
            : base(CdsError.ADRStatusIsNotActive, detail, System.Net.HttpStatusCode.Forbidden, message)
        {
        }

        public AdrStatusNotActiveException(string detail)
          : base(CdsError.ADRStatusIsNotActive, detail, System.Net.HttpStatusCode.Forbidden, null)
        {
        }

        public AdrStatusNotActiveException(SoftwareProductStatus status)
        : base(CdsError.ADRStatusIsNotActive, Constants.ErrorMessages.General.SoftwareProductStatusInactive.Replace("{0}", status.ToEnumMemberAttrValue()), System.Net.HttpStatusCode.Forbidden, null)
        {
        }

        public AdrStatusNotActiveException(LegalEntityStatus status)
        : base(CdsError.ADRStatusIsNotActive, Constants.ErrorMessages.General.SoftwareProductStatusInactive.Replace("{0}", status.ToEnumMemberAttrValue()), System.Net.HttpStatusCode.Forbidden, null) // TODO: Should the message be Legal Entity Status instead of Software Product? Noted in Bug 63710
        {
        }
    }
}
