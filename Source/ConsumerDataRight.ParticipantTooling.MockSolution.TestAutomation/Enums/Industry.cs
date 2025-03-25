namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Industry
    {
        [EnumMember(Value = "Banking")]
        BANKING,

        [EnumMember(Value = "Energy")]
        ENERGY,

        [EnumMember(Value = "All")]
        ALL,
    }
}


