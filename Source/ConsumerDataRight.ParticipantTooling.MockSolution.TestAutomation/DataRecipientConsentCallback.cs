using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    public class DataRecipientConsentCallback
    {
        public DataRecipientConsentCallback(string redirectUrl)
        {
            RedirectUrl = redirectUrl;

            Request = new CallbackRequest
            {
                PathAndQuery = new Uri(redirectUrl).PathAndQuery
            };
        }

        public string RedirectUrl { get; init; }
        private string RedirectUrlLeftPart => new Uri(RedirectUrl).GetLeftPart(UriPartial.Authority);

        private IWebHost? _host;

        public class CallbackRequest
        {
            public string? PathAndQuery { get; init; }
            public bool received = false;
            public HttpMethod? method;
            public string? body;
            public string? queryString;
        }
        private CallbackRequest Request { get; init; }

        /// <summary>
        /// Start web host
        /// </summary>
        public void Start()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Start), nameof(DataRecipientConsentCallback));

            _host = new WebHostBuilder()
               .ConfigureServices(s => { s.AddSingleton(typeof(CallbackRequest), Request); })
               .UseKestrel()
               .UseStartup<DataRecipientConsentCallbackStartup>()
               .UseUrls(RedirectUrlLeftPart)
               .Build();

            _host.RunAsync();
        }

        /// <summary>
        /// Stop web host
        /// </summary>
        public async Task Stop()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Stop), nameof(DataRecipientConsentCallback));

            if (_host != null)
            {
                await _host.StopAsync();
            }
        }

        /// <summary>
        /// Wait until we get a callback or otherwise timeout
        /// </summary>
        public async Task<CallbackRequest?> WaitForCallback(int timeoutSeconds = 30)
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(WaitForCallback), nameof(DataRecipientConsentCallback));

            var stopAt = DateTime.Now.AddSeconds(timeoutSeconds);

            // Keep checking until we timeout
            while (DateTime.Now < stopAt)
            {
                // Have we received the callback?
                if (Request.received)
                {
                    // Yes, so return the content
                    return Request;
                }

                // Otherwise wait another second
                await Task.Delay(1000);
            }

            return null; // Timed out
        }

        class DataRecipientConsentCallbackStartup
        {
            readonly CallbackRequest _callbackRequest;

            public DataRecipientConsentCallbackStartup(CallbackRequest callbackRequest)
            {
                _callbackRequest = callbackRequest;
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet(_callbackRequest.PathAndQuery!, async context =>
                    {
                        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                        _callbackRequest.method = HttpMethod.Get;
                        _callbackRequest.body = body;
                        _callbackRequest.queryString = context.Request.QueryString.Value;
                        _callbackRequest.received = true;
                    });

                    endpoints.MapPost(_callbackRequest.PathAndQuery!, async context =>
                    {
                        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                        _callbackRequest.method = HttpMethod.Post;
                        _callbackRequest.body = body;
                        _callbackRequest.queryString = context.Request.QueryString.Value;
                        _callbackRequest.received = true;
                    });
                });
            }

            public static void ConfigureServices(IServiceCollection services)
            {
                services.AddRouting();
            }
        }
    }
}