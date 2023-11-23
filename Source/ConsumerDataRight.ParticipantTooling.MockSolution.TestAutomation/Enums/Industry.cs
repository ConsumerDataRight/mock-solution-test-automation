using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
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


