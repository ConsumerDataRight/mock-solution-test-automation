using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Energy
{
    /// <summary>
    /// Used by Energy Accounts/Plans
    /// </summary>
    public class PlanOverview
    {
        /// <summary>
        /// The name of the plan if one exists.
        /// </summary>
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
        /// <summary>
        /// The start date of the applicability of this plan
        /// </summary>
        [JsonProperty("startDate", Required = Required.Always)]
        public string StartDate { get; set; }
        /// <summary>
        /// The end date of the applicability of this plan
        /// </summary>
        [JsonProperty("endDate")]
        public string? EndDate { get; set; }
    }
}
