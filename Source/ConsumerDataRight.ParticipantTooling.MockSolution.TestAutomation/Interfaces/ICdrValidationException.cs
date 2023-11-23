using System.ComponentModel.DataAnnotations;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface ICdrValidationException<T>
    {
        List<T> Items { get; set; }

        List<ValidationResult> ValidationErrors { get; set; }
    }
}