namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public interface ISqlQueryService
    {
        string GetClientId(string softwareProductId);

        string GetStatus(EntityType entityType, string id);

        void SetStatus(EntityType entityType, string id, string status);
    }
}