namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseMode
    {
        [EnumMember(Value = "jwt")]
        Jwt,

        // Invalid values used for testing
        [EnumMember(Value = "form_post")]
        FormPost,

        [EnumMember(Value = "fragment")]
        Fragment,

        [EnumMember(Value = "query")]
        Query,

        [EnumMember(Value = "form_post.jwt")]
        FormPostJwt,

        [EnumMember(Value = "fragment.jwt")]
        FragmentJwt,

        [EnumMember(Value = "query.jwt")]
        QueryJwt,

        [EnumMember(Value = "foo")]
        TestOnlyFoo,
    }
}
