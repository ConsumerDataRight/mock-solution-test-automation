namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Fixtures
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
    using Microsoft.Extensions.Options;
    using Serilog;
    using Xunit;

    /// <summary>
    /// Purges DataHolders AuthServer database and registers software product
    /// (in addition to operations performed by TestFixture).
    /// </summary>
    public class RegisterSoftwareProductFixture : BaseFixture, IAsyncLifetime
    {
        private readonly TestAutomationOptions _options;
        private readonly IDataHolderRegisterService _dataHolderRegisterService;
        private readonly IDataHolderAccessTokenCache _dataHolderAccessTokenCache;

        public RegisterSoftwareProductFixture(
           IOptions<TestAutomationOptions> options,
           IDataHolderRegisterService dataHolderRegisterService,
           IDataHolderAccessTokenCache dataHolderAccessTokenCache)
            : base(options)
        {
            Log.Information("Constructing {ClassName}.", nameof(RegisterSoftwareProductFixture));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _dataHolderRegisterService = dataHolderRegisterService ?? throw new ArgumentNullException(nameof(dataHolderRegisterService));
            _dataHolderAccessTokenCache = dataHolderAccessTokenCache ?? throw new ArgumentNullException(nameof(dataHolderAccessTokenCache));
        }

        new public async Task InitializeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(InitializeAsync), nameof(RegisterSoftwareProductFixture));

            // Any Access Tokens in cache will become invalid when the database is purged.
            _dataHolderAccessTokenCache.ClearCache();

            await base.InitializeAsync();

            // Purge AuthServer
            Helpers.AuthServer.PurgeAuthServerForDataholder(_options);

            // Register software product
            await _dataHolderRegisterService.RegisterSoftwareProduct(responseType: ResponseType.Code);
        }

        new public async Task DisposeAsync()
        {
            Log.Information(Constants.LogTemplates.StartedFunctionInClass, nameof(DisposeAsync), nameof(RegisterSoftwareProductFixture));

            await base.DisposeAsync();
        }
    }
}
