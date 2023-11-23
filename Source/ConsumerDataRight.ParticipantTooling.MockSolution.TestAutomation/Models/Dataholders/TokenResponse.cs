using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Dataholders
{

    public class TokenResponse
    {
        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("id_token")]
        public string? IdToken { get; set; }

        [JsonProperty("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonProperty("cdr_arrangement_id")]
        public string? CdrArrangementId { get; set; }

        [JsonProperty("scope")]
        public string? Scope { get; set; }
    };

}
