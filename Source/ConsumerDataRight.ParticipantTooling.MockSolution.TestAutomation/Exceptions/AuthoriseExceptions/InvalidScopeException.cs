namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    public class InvalidScopeException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidScopeException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidScopeException()
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_scope", "Additional scopes were requested in the refresh_token request") //TODO: Should a error gen code and add to constants. Bug 64146
        { }
    }
}
