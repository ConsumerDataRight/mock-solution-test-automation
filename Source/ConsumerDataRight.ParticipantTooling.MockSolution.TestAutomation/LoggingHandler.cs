namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    using Serilog;

    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Log.Debug("{Function} Request: {Request}", nameof(HttpClient.SendAsync), Newtonsoft.Json.JsonConvert.SerializeObject(request));

            var response = await base.SendAsync(request, cancellationToken);

            Log.Debug("{Function} Response: StatusCode={StatusCode} \nContent={Content}", nameof(HttpClient.SendAsync), response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));

            return response;
        }
    }
}
