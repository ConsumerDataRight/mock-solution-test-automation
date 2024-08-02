using System.ComponentModel.DataAnnotations;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;

namespace Register.Common.Exceptions
{
    public class CdrValidationException<T> : Exception, ICdrValidationException<T>
    {
        public CdrValidationException()
        {
        }

        public CdrValidationException(string message)
            : base(message)
        {
        }

        public CdrValidationException(string message, List<T> items, List<ValidationResult> validationErrors)
            : base(message)
        {
            Items = items;
            ValidationErrors = validationErrors;
        }

        public List<T> Items { get; set; }

        public List<ValidationResult> ValidationErrors { get; set; }
    }
}
