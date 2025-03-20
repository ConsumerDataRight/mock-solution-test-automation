namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Energy
{
    using Newtonsoft.Json;

    public class Data
    {
        /// <summary>
        /// Array of accounts.
        /// </summary>
        [JsonProperty("accounts", Required = Required.Always)]
        public EnergyAccountV2[] Accounts { get; set; }
    }
}
