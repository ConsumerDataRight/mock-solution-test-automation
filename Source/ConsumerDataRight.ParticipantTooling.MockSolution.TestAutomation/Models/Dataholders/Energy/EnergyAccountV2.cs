namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders.Energy
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class EnergyAccountV2
    {
        /// <summary>
        /// A unique ID of the account adhering to the standards for ID permanence.
        /// </summary>
        [JsonProperty("accountId", Required = Required.Always)]
        public string AccountId { get; set; }

        /// <summary>
        /// Optional identifier of the account as defined by the data holder.
        /// This must be the value presented on physical statements (if it exists) and must not be used for the value of accountId.
        /// </summary>
        [JsonProperty("accountNumber")]
        public string? AccountNumber { get; set; }

        /// <summary>
        /// The date that the account was created or opened. Mandatory if openStatus is OPEN.
        /// </summary>
        [JsonProperty("creationDate")]
        public string? CreationDate { get; set; }

        /// <summary>
        /// An optional display name for the account if one exists or can be derived.
        /// The content of this field is at the discretion of the data holder.
        /// </summary>
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Open or closed status for the account. If not present then OPEN is assumed.
        /// </summary>
        [JsonProperty("openStatus")]
        public string? OpenStatus { get; set; }

        /// <summary>
        /// The array of plans containing service points and associated plan details.
        /// </summary>
        [JsonProperty("plans", Required = Required.Always)]
        public Plan[] Plans { get; set; }
    }
}
