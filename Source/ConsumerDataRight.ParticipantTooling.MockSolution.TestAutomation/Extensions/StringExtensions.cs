using IdentityModel;
using Serilog;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class StringExtensions
    {
        static public void WriteStringToFile(string filename, string? str)
        {
            Log.Information("Writing string value to filename: {filename}", filename);
            File.WriteAllText(filename, str);
        }

        /// <summary>
        /// Convert string to int
        /// </summary>
        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static string CreatePkceChallenge(this string codeVerifier)
        {
            using (var sha256 = SHA256.Create())
            {
                var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                return Base64Url.Encode(challengeBytes);
            }
        }

        public static bool IsNullOrWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

    }
}
