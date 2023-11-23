using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{

    public static class TokenTypeExtensions
    {
        public static string GetUserIdByTokenType(this TokenType tokenType)
        {
            try
            {
                return tokenType switch
                {
                    TokenType.JaneWilson => Constants.Users.Banking.UserIdJaneWilson, //Banking
                    TokenType.MaryMoss => Constants.Users.Energy.UserIdMaryMoss, //Energy
                    TokenType.HeddaHare => Constants.Users.Energy.UserIdHeddaHare,
                    TokenType.SteveKennedy => Constants.Users.UserIdSteveKennedy,
                    TokenType.DewayneSteve => Constants.Users.UserIdDewayneSteve,
                    TokenType.Business1 => Constants.Users.UserIdBusiness1,
                    TokenType.Business2 => Constants.Users.UserIdBusiness2,
                    TokenType.Beverage => Constants.Users.UserIdBeverage,
                    TokenType.KamillaSmith => Constants.Users.UserIdKamillaSmith,
                    _ => throw new ArgumentException($"{nameof(GetUserIdByTokenType)} failed for {nameof(TokenType)}={tokenType}.")
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public static string GetAllAccountIdsByTokenType(this TokenType tokenType)
        {
            try
            {
                return tokenType switch
                {
                    TokenType.JaneWilson => Constants.Accounts.Banking.AccountIdsAllJaneWilson, //Banking
                    TokenType.MaryMoss => Constants.Accounts.Energy.AccountIdsAllMaryMoss,  //Energy
                    TokenType.HeddaHare => Constants.Accounts.Energy.AccountIdsAllHeddaHare,//Energy
                    TokenType.SteveKennedy => Constants.Accounts.AccountIdsAllSteveKennedy,
                    TokenType.DewayneSteve => Constants.Accounts.AccountIdsAllDewayneSmith,
                    TokenType.Business1 => Constants.Accounts.AccountIdsAllBusiness1,
                    TokenType.Business2 => Constants.Accounts.AccountIdsAllBusiness2,
                    TokenType.Beverage => Constants.Accounts.AccountIdsAllBeverage,
                    TokenType.KamillaSmith => Constants.Accounts.AccountIdsAllKamillaSmith,
                    _ => throw new ArgumentException($"{nameof(GetAllAccountIdsByTokenType)} failed for {nameof(TokenType)}={tokenType}.")
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }
    }


}
