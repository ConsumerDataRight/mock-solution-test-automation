using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseType
    {
        [EnumMember(Value = "code id_token")]
        CodeIdToken,
        [EnumMember(Value = "code")]
        Code,

        //Nonsense values used for testing
        [EnumMember(Value = "Foo")]
        TestOnlyFoo,
        [EnumMember(Value = "token")]
        TestOnlyToken,
        [EnumMember(Value = "code token")]
        TestOnlyCodeToken,
        [EnumMember(Value = "id_token token")]
        TestOnlyIdTokenToken,
        [EnumMember(Value = "code id_token token")]
        TestOnlyCodeIdTokenToken,
        [EnumMember(Value = "code Foo")]
        TestOnlyCodeFoo,
        [EnumMember(Value = "code id_token Foo")]
        TestOnlyCodeIdTokenFooo,
    }
}
