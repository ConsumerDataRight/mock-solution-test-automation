using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using Microsoft.Playwright;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation
{
    public class AuthenticateLoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _hedMockDataHolderHeading;
        private readonly ILocator _txtCustomerId;
        private readonly ILocator _btnContinue;
        private readonly ILocator _lblHelpForExampleUserNames;

        public AuthenticateLoginPage(IPage page, bool waitForPageToLoad = true)
        {
            _page = page;
            _hedMockDataHolderHeading = _page.Locator("h6:has-text(\"Mock Data Holder\")", true);
            _txtCustomerId = _page.Locator("id=customerId", true);
            _btnContinue = _page.Locator("button:has-text(\"Continue\")", true);
            _lblHelpForExampleUserNames = _page.Locator("//div[@role='alert']", true);

            if (waitForPageToLoad)
            {
                _hedMockDataHolderHeading.WaitForAsync().Wait();
            }
        }

        public async Task EnterCustomerId(string customerId)
        {
            await _txtCustomerId.WaitForAsync();
            await Task.Delay(1000); //require for JS delayed defaulting of field. It can sometimes overwrite the entered value.
            await _txtCustomerId.FillAsync("");
            await _txtCustomerId.FillAsync(customerId);
        }

        public async Task ClickContinue()
        {
            await _btnContinue.ClickAsync();
        }

        public async Task<string> GetHelpForExampleUserNamesText()
        {
            return await _lblHelpForExampleUserNames.TextContentAsync();
        }

        public async Task<bool> CustomerIdErrorExists(string errorToCheckFor)
        {
            try
            {
                var element = await _page.WaitForSelectorAsync($"//p[text()='{errorToCheckFor}']", true);

                return await element.IsVisibleAsync();
            }
            catch (TimeoutException) { }
            {
                Log.Error("A timeout exception was caught in {Class}.{Function}", nameof(AuthenticateLoginPage), nameof(CustomerIdErrorExists));
                return false;
            }
        }

    }
}
