using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class PageElementNotFoundException : Exception
    { 
        public PageElementNotFoundException(string page, string selector) : base($"{page} page element could not be found using selector: {selector}")
        { }

        protected PageElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
