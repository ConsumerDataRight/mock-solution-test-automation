using Microsoft.Playwright;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UI
{
    public class PlaywrightDriver
    {
        public IBrowser Browser { get; set; } = null!;
        private IPlaywright PlaywrightInstance { get; set; } = null!;
        private IBrowserContext? _browserContext;
        private string? _mediaFilePrefix;
        private const string _mediaFilePath = "/testresults/media";

        public async Task<IBrowserContext> NewBrowserContext(string? mediaFilePrefix = null)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(NewBrowserContext), nameof(PlaywrightDriver));

            // mediaFilePrefix is usually set to the name of a test case.
            // If media file prefix not provided, use a Guid.
            if (String.IsNullOrEmpty(mediaFilePrefix))
            {
                _mediaFilePrefix = Guid.NewGuid().ToString();
            }
            else
            {
                _mediaFilePrefix = mediaFilePrefix;
            }
            
            PlaywrightInstance = await Playwright.CreateAsync();
            Browser = await GetBrowser();

            var options = new BrowserNewContextOptions
            {
                IgnoreHTTPSErrors = true,                
            };

            options.ViewportSize = new ViewportSize
            {
                Width = 2000,
                Height = 1000
            };            

            _browserContext = await Browser.NewContextAsync(options);

            await _browserContext.Tracing.StartAsync(new()
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true,
            });

            return _browserContext;
        }

        private async Task<IBrowser> GetBrowser()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(GetBrowser), nameof(PlaywrightDriver));

            if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER")?.ToUpper() == "TRUE")
            {
                return await PlaywrightInstance.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true,
                });
            }
            else
            {
                return await PlaywrightInstance.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                });
            }       
        }

        public async Task DisposeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(DisposeAsync), nameof(PlaywrightDriver));

            if (_browserContext != null)
            {
                await _browserContext.Tracing.StopAsync(new()
                {
                    Path = $"{_mediaFilePath}/{_mediaFilePrefix}_trace.zip",
                });

                await Browser.CloseAsync();
                await Browser.DisposeAsync();
                PlaywrightInstance.Dispose();
            }
        }
    }
}
