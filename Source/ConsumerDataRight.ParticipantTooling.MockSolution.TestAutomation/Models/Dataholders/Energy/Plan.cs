using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Energy
{
    /// <summary>
    /// Used by Energy Accounts
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Optional display name for the plan provided by the customer to help differentiate multiple plans.
        /// </summary>
        [JsonProperty("nickname")]
        public string? Nickname { get; set; }
        /// <summary>
        /// An array of servicePointIds, representing NMIs, that this plan is linked to. 
        /// If there are no service points allocated to this plan then an empty array would be expected.
        /// </summary>
        [JsonProperty("servicePointIds", Required = Required.Always)]
        public string[] ServicePointIds { get; set; }
        /// <summary>
        /// Mandatory if openStatus is OPEN
        /// </summary>
        [JsonProperty("planOverview")]
        public PlanOverview? PlanOverview { get; set; }
    }
}
