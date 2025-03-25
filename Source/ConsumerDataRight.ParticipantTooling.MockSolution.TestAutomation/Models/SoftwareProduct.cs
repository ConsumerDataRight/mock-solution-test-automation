namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    using Newtonsoft.Json;

    public class SoftwareProduct
    {
        [JsonProperty("softwareProductId")]
        public string SoftwareProductId { get; set; }

        [JsonProperty("softwareProductName")]
        public string SoftwareProductName { get; set; }

        [JsonProperty("softwareProductDescription")]
        public string SoftwareProductDescription { get; set; }

        [JsonProperty("logoUri")]
        public string LogoUri { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
