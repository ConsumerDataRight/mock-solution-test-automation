namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums
{
    using System.Runtime.Serialization;

    public enum LegalEntityStatus
    {
        [EnumMember(Value = "ACTIVE")]
        ACTIVE,
        [EnumMember(Value = "INACTIVE")]
        INACTIVE,
        [EnumMember(Value = "REMOVED")]
        REMOVED,
    }
}
