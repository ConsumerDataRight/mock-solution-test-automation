using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Services
{
    public class PrivateKeyJwtService : IPrivateKeyJwtService
    {
        public bool RequireIssuer { get; init; } = true;
        public string CertificateFilename { get; set; }
        public string CertificatePassword { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public string Generate()
        {
            var claims = new List<Claim>
            {
                new Claim("sub", Issuer),
                new Claim("jti", Guid.NewGuid().ToString()),
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer)
            };

            return Generate(claims, DateTime.UtcNow.AddMinutes(10));
        }

        private string Generate(IEnumerable<Claim> claims, DateTime expires)
        {
            Log.Information("Calling {FUNCTION} in {ClassName}.", nameof(Generate), nameof(PrivateKeyJwtService));

            var certificate = new X509Certificate2(CertificateFilename, CertificatePassword, X509KeyStorageFlags.Exportable);

            var x509SigningCredentials = new X509SigningCredentials(certificate, SecurityAlgorithms.RsaSsaPssSha256);

            if (RequireIssuer)
            {
                if (string.IsNullOrEmpty(Issuer))
                {
                    throw new ArgumentException("issuer must be provided");
                }
            }

            if (string.IsNullOrEmpty(Audience))
            {
                throw new ArgumentException("audience must be provided");
            }

            var jwt = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                expires: expires,
                signingCredentials: x509SigningCredentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(jwt);
        }
    }
}
