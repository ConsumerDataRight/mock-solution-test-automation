namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using System.ComponentModel.DataAnnotations;

    public interface ICdrValidationException<T>
    {
        List<T> Items { get; set; }

        List<ValidationResult> ValidationErrors { get; set; }
    }
}