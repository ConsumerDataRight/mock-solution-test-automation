using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Energy
{
    public class ResponseEnergyAccountListV2
    {
        /// <summary>
        /// Array of accounts.
        /// </summary>
        [JsonProperty("data", Required = Required.Always)]
        public Data Data { get; set; }

        [JsonProperty("links", Required = Required.Always)]
        public LinksPaginated Links { get; set; }

        [JsonProperty("meta", Required = Required.Always)]
        public MetaPaginated Meta { get; set; }
    }
}
