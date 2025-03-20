namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    public class PageElementNotFoundException : Exception
    {
        public PageElementNotFoundException(string page, string selector)
            : base($"{page} page element could not be found using selector: {selector}")
        {
        }
    }
}
