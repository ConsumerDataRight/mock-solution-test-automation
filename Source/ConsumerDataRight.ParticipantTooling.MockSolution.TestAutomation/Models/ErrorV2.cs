using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    public class ErrorV2
    {
        public ErrorV2(string code, string title, string detail, string? metaUrn)
        {
            Code = code;
            Title = title;
            Detail = detail;
            Meta = new object(); //TODO: This is a workaround. Meta should actually use the code below (matching with RAAP) but the PT APIs use this code, so until it is resolved we should match the test behaviour to the PT API. Bug 64152
            //Meta = metaUrn.IsNullOrWhiteSpace() ? null : new ErrorMetaV2(metaUrn);
        }

        /// <summary>
        /// Error code.
        /// </summary>
        [JsonProperty("code")]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Error title.
        /// </summary>
        [JsonProperty("title")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Error detail.
        /// </summary>
        [JsonProperty("detail")]
        [Required]
        public string Detail { get; set; }

        /// <summary>
        /// Optional additional data for specific error types.
        /// </summary>
        [JsonProperty("meta")]
        public object Meta { get; set; }
    }
}
