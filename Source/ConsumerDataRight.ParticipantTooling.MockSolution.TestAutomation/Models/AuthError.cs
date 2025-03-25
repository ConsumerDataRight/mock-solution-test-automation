namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This error is used for deserializing errors that we get during the authetication process, so that we can check the properties against exceptions instead of hard-coded strings.
    /// </summary>
    public class AuthError
    {
        [JsonProperty("error")]
        public string Code { get; set; }

        [JsonProperty("error_description")]
        public string? Description { get; set; }

        public AuthError(string code, string? description = null)
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
