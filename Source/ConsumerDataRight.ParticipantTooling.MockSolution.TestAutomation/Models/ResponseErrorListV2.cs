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

        public ResponseErrorListV2(CdrException ex)
        {
            var error = new ErrorV2(ex.Code, ex.Title, ex.Detail, ex.Message);
            Errors = new List<ErrorV2>() { error };
        }

        [JsonProperty("errors")]
        [Required]
        public List<ErrorV2> Errors { get; set; }
    }
}
