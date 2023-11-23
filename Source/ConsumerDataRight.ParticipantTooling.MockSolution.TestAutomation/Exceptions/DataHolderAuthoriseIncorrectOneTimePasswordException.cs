using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class DataHolderAuthoriseIncorrectOneTimePasswordException : Exception
    {
        protected DataHolderAuthoriseIncorrectOneTimePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
