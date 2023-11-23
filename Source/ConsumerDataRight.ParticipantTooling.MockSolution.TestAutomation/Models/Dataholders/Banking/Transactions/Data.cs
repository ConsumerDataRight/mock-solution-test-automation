using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Transactions
{
    public class Data
    {
        /// <summary>
        /// Array of transactions
        /// </summary>
        [JsonProperty("transactions", Required = Required.Always)]
        public BankingTransaction[] Transactions { get; set; }
    }
}
