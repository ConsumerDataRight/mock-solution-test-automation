using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders
{
    public class MetaPaginated
    {
        /// <summary>
        /// The total number of records in the full set.
        /// </summary>
        [JsonProperty("totalRecords", Required = Required.Always)]
        public long TotalRecords { get; set; }
        /// <summary>
        /// The total number of pages in the full set.
        /// </summary>
        [JsonProperty("totalPages", Required = Required.Always)]
        public long TotalPages { get; set; }
    }
}
