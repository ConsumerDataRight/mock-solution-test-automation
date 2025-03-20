namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Fixtures
{
    using Serilog;
    using Xunit;

    public class PlaywrightFixture : IAsyncLifetime
    {
        static private bool RUNNING_IN_CONTAINER => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.ToUpper() == "TRUE";

        virtual public Task InitializeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(InitializeAsync), nameof(PlaywrightFixture));

            // Only install Playwright if not running in container, since Dockerfile.e2e-tests already installed Playwright
            if (!RUNNING_IN_CONTAINER)
            {
                // Ensure that Playwright has been fully installed.
                Microsoft.Playwright.Program.Main(new string[] { "install" });
                Microsoft.Playwright.Program.Main(new string[] { "install-deps" });
            }

            return Task.CompletedTask;
        }

        virtual public Task DisposeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(DisposeAsync), nameof(PlaywrightFixture));

            return Task.CompletedTask;
        }
    }
}
