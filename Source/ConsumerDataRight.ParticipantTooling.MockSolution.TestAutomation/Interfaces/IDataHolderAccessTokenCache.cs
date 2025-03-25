namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public interface IDataHolderAccessTokenCache
    {
        int Hits { get; }

        int Misses { get; }

        void ClearCache();

        Task<string?> GetAccessToken(TokenType tokenType, string? scope = null, bool useCache = true, ResponseType responseType = ResponseType.Code, ResponseMode responseMode = ResponseMode.Jwt);
    }
}