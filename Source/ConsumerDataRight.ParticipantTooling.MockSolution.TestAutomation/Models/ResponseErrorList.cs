namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    using System.ComponentModel.DataAnnotations;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions;
    using Newtonsoft.Json;

    public class ResponseErrorList
    {
        public ResponseErrorList()
        {
            Errors = new List<Error>() { };
        }

        public ResponseErrorList(Error error)
        {
            Errors = new List<Error>() { error };
        }

        public ResponseErrorList(CdsError error, string errorDetail)
        {
            var errorInfo = error.GetErrorInfo();

            Errors = new List<Error>()
            {
                new Error(errorInfo.ErrorCode, errorInfo.Title, errorDetail),
            };
        }

        public ResponseErrorList(string errorCode, string errorTitle, string errorDetail)
        {
            var error = new Error(errorCode, errorTitle, errorDetail);
            Errors = new List<Error>() { error };
        }

        [JsonProperty("errors")]
        [Required]
        public List<Error> Errors { get; set; }
    }
}
