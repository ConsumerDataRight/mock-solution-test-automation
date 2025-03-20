namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
    using Microsoft.Playwright;

    public class ConfirmAccountSharingPage
    {
        private readonly IPage _page;
        private readonly ILocator _btnAuthorise;
        private readonly ILocator _btnDeny;

        public ConfirmAccountSharingPage(IPage page, bool waitForPageToLoad = true)
        {
            _page = page;
            _btnAuthorise = _page.Locator("text=Authorise", true);
            _btnDeny = _page.Locator("text=Deny", true);

            if (waitForPageToLoad)
            {
                _page.WaitForURLAsync($"**/confirmation");
            }
        }

        public async Task ClickAuthorise()
        {
            await _btnAuthorise.ClickAsync();
        }

        public async Task ClickDeny()
        {
            await _btnDeny.ClickAsync();
        }

        public async Task ClickCLusterHeadingToExpand(string clusterHeading)
        {
            await _page.Locator($"//a[text()='{clusterHeading}']", true).ClickAsync();
        }

        public async Task<string> GetClusterDetail(string clusterHeading)
        {
            var stopAt = DateTime.Now.AddSeconds(10);
            string clusterDetail = string.Empty;

            // Need a custom sync to work around issue where cluster details sometimes do not load in time.
            // Retry for up to 10 Seconds to get a non blank value for cluser details.
            while (DateTime.Now < stopAt && String.IsNullOrEmpty(clusterDetail))
            {
                await Task.Delay(100);
                clusterDetail = await _page.Locator($"//div[@role='button' and .//a[text()='{clusterHeading}']]/..//p", true).InnerTextAsync();
            }

            return clusterDetail;
        }

        public async Task<int> GetClusterCount()
        {
            var allClusterHeadings = await _page.QuerySelectorAllAsync("//div[contains(@class,'MuiAccordionSummary')]/a");
            return allClusterHeadings.Count;
        }
    }
}
