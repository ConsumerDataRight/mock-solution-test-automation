using System.ComponentModel.DataAnnotations;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions;
using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    public class ResponseErrorListV2
    {
        public ResponseErrorListV2()
        {
            Errors = new List<ErrorV2>();
        }

        public ResponseErrorListV2(ErrorV2 error)
        {
            Errors = new List<ErrorV2>() { error };
        }

        public ResponseErrorListV2(string errorCode, string errorTitle, string errorDetail, string? metaUrn)
        {
            var error = new ErrorV2(errorCode, errorTitle, errorDetail, metaUrn);
            Errors = new List<ErrorV2>() { error };
        }

        /// <summary>
        /// A straight conversion of the Exception into an error response, which will include a message similar to 'An exception of type 'Exception' was thrown' as the urn of the error's meta property
        /// </summary>
        /// <param name="ex">The exception that was thrown</param>
        public ResponseErrorListV2(CdrException ex)
        {
            var error = new ErrorV2(ex.Code, ex.Title, ex.Detail, ex.Message);
            Errors = new List<ErrorV2>() { error };
        }

        /// <summary>
        /// Allows for a custom (or empty) error message to be set, as the ex.message from a thrown exception may not be desired
        /// </summary>
        /// <param name="ex">The exception that was thrown</param>
        /// <param name="message">The message that will show up as the urn of the error's meta property</param>
        public ResponseErrorListV2(CdrException ex, string message)
        {
            var error = new ErrorV2(ex.Code, ex.Title, ex.Detail, message);
            Errors = new List<ErrorV2>() { error };
        }

        [JsonProperty("errors")]
        [Required]
        public List<ErrorV2> Errors { get; set; }
    }
}
