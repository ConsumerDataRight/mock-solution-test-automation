namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI.Pages.Authorisation;
    using Microsoft.Playwright;

    public static class PlaywrightExtensions
    {
        public static ILocator Locator(this IPage? page, string selector, bool throwErrorIfNotFound)
        {
            var result = page?.Locator(selector);

            if (throwErrorIfNotFound && result == null)
            {
                throw new PageElementNotFoundException(nameof(AuthenticateLoginPage), selector).Log();
            }

            return result;
        }

        public static async Task<IElementHandle?> WaitForSelectorAsync(this IPage? page, string selector, bool throwErrorIfNotFound)
        {
            var result = await page?.WaitForSelectorAsync(selector);

            if (throwErrorIfNotFound && result == null)
            {
                throw new PageElementNotFoundException(nameof(AuthenticateLoginPage), selector).Log();
            }

            return result;
        }
    }
}
