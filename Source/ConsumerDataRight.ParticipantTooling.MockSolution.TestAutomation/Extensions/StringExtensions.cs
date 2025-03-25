namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    using System.Security.Cryptography;
    using System.Text;
    using IdentityModel;
    using Serilog;

    public static class StringExtensions
    {
        static public void WriteStringToFile(string filename, string? str)
        {
            Log.Information("Writing string value to filename: {Filename}", filename);
            File.WriteAllText(filename, str);
        }

        /// <summary>
        /// Convert string to int.
        /// </summary>
        /// <returns>int.</returns>
        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static string CreatePkceChallenge(this string codeVerifier)
        {
            var challengeBytes = SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
            return Base64Url.Encode(challengeBytes);
        }

        public static bool IsNullOrWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
