namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    using System.Net;
    using Newtonsoft.Json;

    public class AuthoriseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        [JsonProperty("error")]
        public string Error { get; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; }

        public AuthoriseException()
        {
        }

        public AuthoriseException(string message)
            : base(message)
        {
        }

        public AuthoriseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AuthoriseException(string message, HttpStatusCode statusCode, string error, string errorDescription)
            : this(message)
        {
            StatusCode = statusCode;
            Error = error;
            ErrorDescription = errorDescription;
        }
    }
}
