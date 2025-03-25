namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseType
    {
        [EnumMember(Value = "code")]
        Code,

        // Invalid values used for testing
        [EnumMember(Value = "code id_token")]
        CodeIdToken,
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
