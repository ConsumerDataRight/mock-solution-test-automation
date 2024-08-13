namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    public class UserCancelledException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCancelledException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public UserCancelledException()
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "access_denied", Constants.ErrorMessages.Authorization.AccessDenied)
        {
        }
    }
}
