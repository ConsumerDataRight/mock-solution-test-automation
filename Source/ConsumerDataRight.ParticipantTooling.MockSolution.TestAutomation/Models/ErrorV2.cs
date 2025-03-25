namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    using System.ComponentModel.DataAnnotations;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using Newtonsoft.Json;

    public class ErrorV2
    {
        public ErrorV2(string code, string title, string detail, string? metaUrn)
        {
            Code = code;
            Title = title;
            Detail = detail;
            Meta = metaUrn.IsNullOrWhiteSpace() ? null : new ErrorMetaV2(metaUrn);
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
        public object? Meta { get; set; }
    }
}
