namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public partial class JwksEndpoint : IAsyncDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JwksEndpoint"/> class.
        /// Emulate a JWKS endpoint on url returning a JWKS for the given certificate.
        /// </summary>
        public JwksEndpoint(string url, string certificateFilename, string certificatePassword)
        {
            Url = url;
            CertificateFilename = certificateFilename;
            CertificatePassword = certificatePassword;
        }

        public string Url { get; init; }

        private string Url_PathAndQuery => new Uri(Url).PathAndQuery;

        private int UrlPort => new Uri(Url).Port;

        public string CertificateFilename { get; init; }

        public string CertificatePassword { get; init; }

        private IWebHost? _host;

        public void Start()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Start), nameof(JwksEndpoint));

            _host = new WebHostBuilder()
                .UseKestrel(opts =>
                {
                    opts.ListenAnyIP(UrlPort, opts => opts.UseHttps());  // This will use the default developer certificate.  Use "dotnet dev-certs https" to install if necessary
                })
               .UseStartup(_ => new JwksCallback_Startup(this))
               .Build();

            _host.RunAsync();
        }

        public async Task Stop()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(Stop), nameof(JwksEndpoint));

            if (_host != null)
            {
                await _host.StopAsync();
            }
        }

        bool _disposed;

        public async ValueTask DisposeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(DisposeAsync), nameof(JwksEndpoint));

            if (!_disposed)
            {
                await Stop();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }

        class JwksCallback_Startup
        {
            private JwksEndpoint Endpoint { get; init; }

            public JwksCallback_Startup(JwksEndpoint endpoint)
            {
                Endpoint = endpoint;
            }

            /// <summary>
            /// This is used by the JWKS endpoint for Startup
            /// NOTE: You may see warnings about 0 references to this function, but it is used 'automatically' as part of the startup process.
            /// </summary>
            /// <param name="app"></param>
            public void Configure(IApplicationBuilder app)
            {
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet(Endpoint.Url_PathAndQuery, async context =>
                    {
                        // Build JWKS and return
                        var jwks = Helpers.Jwk.BuildJWKS(Endpoint.CertificateFilename, Endpoint.CertificatePassword);
                        await context.Response.WriteAsJsonAsync(jwks);
                    });
                });
            }

            /// <summary>
            /// This is used by the JWKS endpoint for Startup
            /// NOTE: You may see warnings about 0 references to this function, but it is used 'automatically' as part of the startup process.
            /// </summary>
            /// <param name="services"></param>
            public static void ConfigureServices(IServiceCollection services)
            {
                services.AddRouting();
            }
        }
    }
}