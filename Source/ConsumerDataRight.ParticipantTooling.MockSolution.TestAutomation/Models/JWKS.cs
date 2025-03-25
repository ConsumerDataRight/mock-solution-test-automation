namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    using Newtonsoft.Json;

    public class Jwks
    {
        [JsonProperty("keys")]
        public Jwk[]? Keys { get; set; }
    }
}