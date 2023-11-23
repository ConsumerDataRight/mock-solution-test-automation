using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    public enum SoftwareProductStatus
    {
        [EnumMember(Value = "ACTIVE")]
        ACTIVE,
        [EnumMember(Value = "INACTIVE")]
        INACTIVE,
        [EnumMember(Value = "REMOVED")]
        REMOVED
    }
}
