using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Playwright;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation
{
    public class SelectAccountsPage
    {

        private readonly IPage _page;
        private readonly ILocator _btnContinue;
        private readonly ILocator _btnCancel;

        public SelectAccountsPage(IPage page, bool waitForPageToLoad = true)
        {
            _page = page;
            _btnContinue = _page.Locator("text=Continue", true);
            _btnCancel = _page.Locator("text=Cancel", true);
            if (waitForPageToLoad)
            {
                _page.WaitForURLAsync($"**/select-accounts");
            }
        }

        public async Task SelectAccount(string accountToSelect)
        {
            if (accountToSelect.IsNullOrEmpty())
            {
                throw new ArgumentOutOfRangeException(nameof(accountToSelect), "Parameter must contain a value.").Log();
            }

            await _page.Locator($"//input[@aria-labelledby='account-{accountToSelect}']", true).CheckAsync();
        }
        public async Task SelectAccounts(string[] accountsToSelect)
        {
            foreach (string accountToSelect in accountsToSelect)
            {
                await SelectAccount(accountToSelect.Trim());
            }

        }
        public async Task SelectAccounts(string accountsToSelectCsv)
        {
            if (accountsToSelectCsv.IsNullOrEmpty())
            {
                throw new ArgumentOutOfRangeException(nameof(accountsToSelectCsv), "Parameter must contain a value.").Log();
            }

            string[]? accountsToSelectArray = accountsToSelectCsv?.Split(",");

            foreach (string accountToSelect in accountsToSelectArray)
            {
                await SelectAccount(accountToSelect.Trim());
            }
        }

        public async Task SelectAllCheckboxes()
        {
            var allInputs = await _page.QuerySelectorAllAsync("//input[@type='checkbox']");

            foreach (var input in allInputs)
            {
                await input.CheckAsync();
            }

        }

        public async Task ClickContinue()
        {
            await _btnContinue.ClickAsync();
        }
        public async Task ClickCancel()
        {
            await _btnCancel.ClickAsync();
        }
        public async Task<bool> NoAccountSelectedErrorExists()
        {
            try
            {
                var element = await _page.WaitForSelectorAsync($"//p[text()='Please select one or more Accounts']",true);

                return await element.IsVisibleAsync();
            }
            catch (TimeoutException) { }
            {
                Log.Error("A timeout exception was caught in {class}.{function}", nameof(SelectAccountsPage), nameof(NoAccountSelectedErrorExists));
                return false;
            }
        }

    }
}
