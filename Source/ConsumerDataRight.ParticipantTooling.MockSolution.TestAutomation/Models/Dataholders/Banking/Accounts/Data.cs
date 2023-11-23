using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Accounts
{
    public class Data
    {
        /// <summary>
        /// Array of accounts
        /// </summary>
        [JsonProperty("accounts", Required = Required.Always)]
        public BankingAccountV2[]? Accounts { get; set; }
    }
}
