namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Accounts
{
    using Newtonsoft.Json;

    public class ResponseBankingAccountListV2
    {
        [JsonProperty("data", Required = Required.Always)]
        public Data Data { get; set; }

        [JsonProperty("links", Required = Required.Always)]
        public LinksPaginated Links { get; set; }

        [JsonProperty("meta", Required = Required.Always)]
        public MetaPaginated Meta { get; set; }
    }
}
