using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    public class DataRecipientBrand
    {
        [JsonProperty("dataRecipientBrandId")]
        public string DataRecipientBrandId { get; set; }
        [JsonProperty("brandName")]
        public string BrandName { get; set; }
        [JsonProperty("logoUri")]
        public string LogoUri { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("softwareProducts")]
        public List<SoftwareProduct> SoftwareProducts { get; set; }
    }

}
