using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    /// <summary>
    /// Access token
    /// </summary>
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("Scope")]
        public string Scope { get; set; }
    }
}
