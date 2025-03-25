namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Banking.Accounts
{
    using Newtonsoft.Json;

    public class Data
    {
        /// <summary>
        /// Array of accounts.
        /// </summary>
        [JsonProperty("accounts", Required = Required.Always)]
        public BankingAccountV2[]? Accounts { get; set; }
    }
}
