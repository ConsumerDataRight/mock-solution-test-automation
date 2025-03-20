namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using System.Runtime.Serialization;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public class UnavailableBankingAccountException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnavailableBankingAccountException"/> class.
        /// <para>Status code: 422 (Unprocessable Entity).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public UnavailableBankingAccountException(string detail, string message)
            : base(CdsError.UnavailableBankingAccount, detail, System.Net.HttpStatusCode.UnprocessableEntity, message)
        {
        }

        public UnavailableBankingAccountException(string detail)
          : base(CdsError.UnavailableBankingAccount, detail, System.Net.HttpStatusCode.UnprocessableEntity, null)
        {
        }
    }
}
