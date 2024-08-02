namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    public class UnexpectedErrorException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidScopeException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public UnexpectedErrorException(string description)
          : base(Enums.CdsError.UnexpectedError, description, System.Net.HttpStatusCode.BadRequest, description)
        { }
    }
}
