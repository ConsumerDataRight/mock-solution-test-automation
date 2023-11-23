using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;

namespace Register.Common.Exceptions
{
    [Serializable]
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

        protected CdrValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public List<T> Items { get; set; }

        public List<ValidationResult> ValidationErrors { get; set; }
    }
}
