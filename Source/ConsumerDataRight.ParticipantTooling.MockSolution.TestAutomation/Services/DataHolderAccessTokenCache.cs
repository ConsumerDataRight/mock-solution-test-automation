using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models.Options;
using Microsoft.Extensions.Options;
using Serilog;
using static ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Constants;
using static System.Formats.Asn1.AsnWriter;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    /// <summary>
    /// Get access token from DataHolder.
    /// Cache request (user/selectedaccounts/scope) and accesstoken.
    /// If cache miss then perform full E2E auth/consent flow to get accesstoken, cache it, and return access token.
    /// If cache hit then use cached access token.
    /// </summary>
    public class DataHolderAccessTokenCache : IDataHolderAccessTokenCache
    {
        private readonly List<CacheItem> _cache = new();
        private readonly IDataHolderParService _dataHolderParService;
        private readonly IDataHolderTokenService _dataHolderTokenService;
        private readonly IApiServiceDirector _apiServiceDirector;
        private readonly TestAutomationOptions _options;
        private readonly TestAutomationAuthServerOptions _authServerOptions;

        public int Hits { get; private set; } = 0;
        public int Misses { get; private set; } = 0;

        public DataHolderAccessTokenCache(IOptions<TestAutomationOptions> options, IOptions<TestAutomationAuthServerOptions> authServerOptions, IDataHolderParService dataHolderParService, IDataHolderTokenService dataHolderTokenService, IApiServiceDirector apiServiceDirector)
        {
            _dataHolderParService = dataHolderParService ?? throw new ArgumentNullException(nameof(dataHolderParService));
            _dataHolderTokenService = dataHolderTokenService ?? throw new ArgumentNullException(nameof(dataHolderTokenService));
            _apiServiceDirector = apiServiceDirector ?? throw new ArgumentNullException(nameof(apiServiceDirector));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _authServerOptions = authServerOptions.Value ?? throw new ArgumentNullException(nameof(authServerOptions));
        }

        class CacheItem
        {
            public string? UserId { get; init; }
            public string? SelectedAccounts { get; init; }
            public string? Scope { get; init; }

            public string? AccessToken { get; set; }
        }

        public async Task<string?> GetAccessToken(TokenType tokenType, string? scope = null, bool useCache = true)
        {
            Log.Information("Calling {FUNCTION} in {ClassName} with Params: {P1}={V1},{P2}={V2},{P3}={V3}.", nameof(GetAccessToken), nameof(DataHolderAccessTokenCache), nameof(tokenType), tokenType, nameof(scope), scope, nameof(useCache), useCache);

            switch (tokenType)
            {
                case TokenType.MaryMoss: //this was used by energy
                case TokenType.HeddaHare:
                case TokenType.JaneWilson: //this was used by banking
                case TokenType.SteveKennedy:
                case TokenType.DewayneSteve:
                case TokenType.Business1:
                case TokenType.Business2:
                case TokenType.Beverage:
                case TokenType.KamillaSmith:
                    {
                        return await GetAccessToken(tokenType.GetUserIdByTokenType(), tokenType.GetAllAccountIdsByTokenType(), scope, useCache);
                    }

                case TokenType.InvalidFoo:
                    return "foo";
                case TokenType.InvalidEmpty:
                    return "";
                case TokenType.InvalidOmit:
                    return null;

                default:
                    throw new ArgumentException($"{nameof(GetAccessToken)} failed for {nameof(TokenType)}={tokenType},{nameof(scope)}={scope},{nameof(useCache)}={useCache}.").Log();
            }
        }

        private async Task<string?> GetAccessToken(
            string userId,
            string selectedAccounts,
            string? scope = null,
            bool useCache = true
            )
        {
            Log.Information("Calling {FUNCTION} in {ClassName} with Params: {P1}={V1},{P2}={V2},{P3}={V3},{P4}={V4}.", nameof(GetAccessToken), nameof(DataHolderAccessTokenCache), nameof(userId), userId, nameof(selectedAccounts), selectedAccounts, nameof(scope), scope, nameof(useCache), useCache);

            if (string.IsNullOrEmpty(scope))
            {
                scope = _options.SCOPE;
            }

            // Find refresh token in cache
            CacheItem? cacheHit = null;
            if (useCache)
            {
                cacheHit = _cache.Find(item =>
                item.UserId == userId &&
                item.SelectedAccounts == selectedAccounts &&
                item.Scope == scope);
            }

            // Cache hit
            if (cacheHit != null)
            {
                Log.Information("{UserId} token was found in the {cache}.", userId, nameof(DataHolderAccessTokenCache));
                Hits++;

                return cacheHit.AccessToken;
            }
            // Cache miss, so perform auth/consent flow to get accesstoken/refreshtoken
            else
            {
                Log.Information("{UserId} token was not found in the {cache}. Performing AuthConsentFlow", userId, nameof(DataHolderAccessTokenCache));
                Misses++;

                (var accessToken, _) = await FromAuthConsentFlow(userId, selectedAccounts, scope);

                // Add refresh token to cache
                _cache.Add(new CacheItem
                {
                    UserId = userId,
                    SelectedAccounts = selectedAccounts,
                    Scope = scope,
                    AccessToken = accessToken
                });

                // Return access token
                return accessToken;
            }
        }

        private async Task<(string accessToken, string refreshToken)> FromAuthConsentFlow(string userId,
            string selectedAccounts,
            string? scope = null)
        {
            if (string.IsNullOrEmpty(scope))
            {
                scope = _options.SCOPE;
            }

            DataHolderAuthoriseService authService;
            if (_options.IS_AUTH_SERVER)
            {
                authService = await new DataHolderAuthoriseService.DataHolderAuthoriseServiceBuilder(_options, _dataHolderParService, _apiServiceDirector, false, _authServerOptions)
                 .WithUserId(userId)
                 .WithScope(scope)
                 .WithSelectedAccountIds(selectedAccounts)
                 .BuildAsync();
            }
            else
            {
                authService = await new DataHolderAuthoriseService.DataHolderAuthoriseServiceBuilder(_options,  _dataHolderParService, _apiServiceDirector)
                .WithUserId(userId)
                .WithScope(scope)
                .WithSelectedAccountIds(selectedAccounts)
                .WithResponseMode(ResponseMode.FormPost)
                .BuildAsync();
            }

            (var authCode, _) = await authService.Authorise();

            // use authcode to get access and refresh tokens
            var tokenResponse = await _dataHolderTokenService.GetResponse(authCode);

            if (tokenResponse?.AccessToken == null)
                throw new InvalidOperationException($"{nameof(FromAuthConsentFlow)} - access token is null").Log();

            if (tokenResponse?.RefreshToken == null)
                throw new InvalidOperationException($"{nameof(FromAuthConsentFlow)} - refresh token is null").Log();

            return (tokenResponse.AccessToken, tokenResponse.RefreshToken);
        }
        public void ClearCache()
        {
            Log.Information("Calling {FUNCTION} in {ClassName}", nameof(ClearCache), nameof(DataHolderAccessTokenCache));
            _cache.Clear();
        }

    }
}
