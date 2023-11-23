using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class DataHolderAuthoriseIncorrectCustomerIdException : Exception
    {
        protected DataHolderAuthoriseIncorrectCustomerIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
