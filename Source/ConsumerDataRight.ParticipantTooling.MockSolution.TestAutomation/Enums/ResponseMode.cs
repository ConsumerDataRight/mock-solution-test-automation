using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseMode
    {
        [EnumMember(Value = "form_post")]
        FormPost,

        [EnumMember(Value = "fragment")]
        Fragment,

        [EnumMember(Value = "query")]
        Query,
        [EnumMember(Value = "jwt")]
        Jwt,
        [EnumMember(Value = "form_post.jwt")]
        FormPostJwt,

        [EnumMember(Value = "fragment.jwt")]
        FragmentJwt,

        [EnumMember(Value = "query.jwt")]
        QueryJwt,

        [EnumMember(Value = "foo")] //Nonsense value used for testing
        TestOnlyFoo
    }
}
