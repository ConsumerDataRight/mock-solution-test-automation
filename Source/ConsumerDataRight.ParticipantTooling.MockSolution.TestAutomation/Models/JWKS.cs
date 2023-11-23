using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    public class Jwks
    {
        [JsonProperty("keys")]
        public Jwk[]? Keys { get; set; }
    }
}