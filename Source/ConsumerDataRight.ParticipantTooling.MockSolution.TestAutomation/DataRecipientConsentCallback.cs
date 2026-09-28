namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class DataRecipientConsentCallback
    {
        public DataRecipientConsentCallback(string redirectUrl)
        {
            RedirectUrl = redirectUrl;

            Request = new CallbackRequest
            {
                PathAndQuery = new Uri(redirectUrl).PathAndQuery,
            };
        }

        public string RedirectUrl { get; init; }

        private string RedirectUrlLeftPart => new Uri(RedirectUrl).GetLeftPart(UriPartial.Authority);

        private IHost? _host;

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
        /// Start web host.
        /// </summary>
        public void Start()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Start), nameof(DataRecipientConsentCallback));

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(s =>
                {
                    s.AddSingleton(typeof(CallbackRequest), Request);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseStartup<DataRecipientConsentCallbackStartup>();
                    webBuilder.UseUrls(RedirectUrlLeftPart);
                })
                .Build();

            _host.RunAsync();
        }

        /// <summary>
        /// Stop web host.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task Stop()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Stop), nameof(DataRecipientConsentCallback));

            if (_host != null)
            {
                await _host.StopAsync();
            }
        }

        /// <summary>
        /// Wait until we get a callback or otherwise timeout.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
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

#pragma warning disable S1118 // Utility classes should not have public constructors
        class DataRecipientConsentCallbackStartup
#pragma warning restore S1118 // Utility classes should not have public constructors
        {
            public static void ConfigureServices(IServiceCollection services)
            {
                services.AddRouting();
            }

            public static void Configure(IApplicationBuilder app, CallbackRequest callbackRequest)
            {
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet(callbackRequest.PathAndQuery!, async context =>
                    {
                        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                        callbackRequest.method = HttpMethod.Get;
                        callbackRequest.body = body;
                        callbackRequest.queryString = context.Request.QueryString.Value;
                        callbackRequest.received = true;
                    });

                    endpoints.MapPost(callbackRequest.PathAndQuery!, async context =>
                    {
                        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                        callbackRequest.method = HttpMethod.Post;
                        callbackRequest.body = body;
                        callbackRequest.queryString = context.Request.QueryString.Value;
                        callbackRequest.received = true;
                    });
                });
            }
        }
    }
}