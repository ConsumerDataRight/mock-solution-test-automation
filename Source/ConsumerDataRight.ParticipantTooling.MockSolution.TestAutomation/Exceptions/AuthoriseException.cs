using System;
using System.Net;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class AuthoriseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        [JsonProperty("error")]
        public string Error { get; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; }

        public AuthoriseException() { }

        public AuthoriseException(string message)
            : base(message) { }

        public AuthoriseException(string message, Exception inner)
            : base(message, inner) { }

        public AuthoriseException(string message, HttpStatusCode statusCode, string error, string errorDescription)
            : this(message)
        {
            StatusCode = statusCode;
            Error = error;
            ErrorDescription = errorDescription;
        }

        protected AuthoriseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
