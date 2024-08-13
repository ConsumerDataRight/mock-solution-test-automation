using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using Microsoft.Playwright;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation
{
    public class OneTimePasswordPage
    {
        private readonly IPage _page;
        private readonly ILocator _txtOneTimePassword;
        private readonly ILocator _btnCancel;
        private readonly ILocator _btnContinue;
        private readonly ILocator _divAlert;
        private readonly ILocator _headDataHolderheading;
        private readonly ILocator _btnCloseAlert;

        public OneTimePasswordPage(IPage page)
        {
            _page = page;
            _txtOneTimePassword = _page.Locator("id=otp", true);
            _btnCancel = _page.Locator("text=Cancel", true);
            _btnContinue = _page.Locator("button:has-text(\"Continue\")", true);
            _divAlert = _page.Locator("//div[@role='alert']", true);
            _headDataHolderheading = _page.Locator("//h6", true);
            _btnCloseAlert = _page.Locator("[role=\"alert\"]>>[title=\"Close\"]", true);

        }
        public async Task EnterOtp(string otp)
        {
            await _txtOneTimePassword.WaitForAsync();
            await _txtOneTimePassword.FillAsync(otp);
        }

        public async Task ClickContinue()
        {
            await _btnContinue.ClickAsync();
        }

        public async Task ClickCancel()
        {
            await _btnCancel.ClickAsync();
        }

        public async Task<string> GetOneTimePasswordFieldValue()
        {
            return await _txtOneTimePassword.InputValueAsync();
        }

        public async Task<string> GetAlertMessage()
        {
            return await _divAlert.InnerTextAsync();
        }

        public async Task<string> GetDataHolderHeading()
        {
            return await _headDataHolderheading.InnerTextAsync();
        }

        public async Task<bool> AlertExists()
        {
            return await _divAlert.IsVisibleAsync();
        }

        public async Task CloseAlertMessage()
        {
            await _btnCloseAlert.ClickAsync();
        }

        public async Task<bool> OtpErrorExists(string errorToCheckFor)
        {
            try
            {
                var element = await _page.WaitForSelectorAsync($"//p[text()='{errorToCheckFor}']",true);

                return await element.IsVisibleAsync();
            }
            catch (TimeoutException) { }
            {
                Log.Error("A timeout exception was caught in {Class}.{Function}",nameof(OneTimePasswordPage),nameof(OtpErrorExists));
                return false;
            }
        }

    }
}
