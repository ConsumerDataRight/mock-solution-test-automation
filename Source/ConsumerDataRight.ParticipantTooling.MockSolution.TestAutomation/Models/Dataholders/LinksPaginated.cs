namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders
{
    using Newtonsoft.Json;

    public class LinksPaginated
    {
        /// <summary>
        /// URI to the first page of this set. Mandatory if this response is not the first page.
        /// </summary>
        [JsonProperty("first")]
        public string? First { get; set; }

        /// <summary>
        /// URI to the last page of this set. Mandatory if this response is not the last page.
        /// </summary>
        [JsonProperty("last")]
        public string? Last { get; set; }

        /// <summary>
        /// URI to the next page of this set. Mandatory if this response is not the last page.
        /// </summary>
        [JsonProperty("next")]
        public string? Next { get; set; }

        /// <summary>
        /// URI to the previous page of this set. Mandatory if this response is not the first page.
        /// </summary>
        [JsonProperty("prev")]
        public string? Prev { get; set; }

        /// <summary>
        /// Fully qualified link that generated the current response document.
        /// </summary>
        [JsonProperty("self", Required = Required.Always)]
        public string Self { get; set; }
    }
}
