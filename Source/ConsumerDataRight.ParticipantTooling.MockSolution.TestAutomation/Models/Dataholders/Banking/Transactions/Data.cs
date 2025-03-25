namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Transactions
{
    using Newtonsoft.Json;

    public class Data
    {
        /// <summary>
        /// Array of transactions.
        /// </summary>
        [JsonProperty("transactions", Required = Required.Always)]
        public BankingTransaction[] Transactions { get; set; }
    }
}
