namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    using System.Runtime.Serialization;

    public enum SoftwareProductStatus
    {
        [EnumMember(Value = "ACTIVE")]
        ACTIVE,
        [EnumMember(Value = "INACTIVE")]
        INACTIVE,
        [EnumMember(Value = "REMOVED")]
        REMOVED,
    }
}
