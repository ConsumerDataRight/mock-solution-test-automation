using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    public class LegalEntity
    {
        [JsonProperty("legalEntityId")]
        public string LegalEntityId { get; set; }
        [JsonProperty("legalEntityName")]
        public string LegalEntityName { get; set; }
        [JsonProperty("logoUri")]
        public string LogoUri { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("dataRecipientBrands")]
        public List<DataRecipientBrand> DataRecipientBrands { get; set; }
    }
}
