﻿namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using FluentAssertions;

    static public class JwtSecurityTokenExtensions
    {
        /// <summary>
        /// Get claim for claimType. Throws exception if no claim or multiple claims (ie must be a single claim for claimType).
        /// </summary>
        /// <returns>Cliam.</returns>
        static public Claim Claim(this JwtSecurityToken jwt, string claimType)
            => jwt.Claims.Single(claim => claim.Type == claimType);

        /// <summary>
        /// Assert JWT contains a claim with given value.
        /// </summary>
        /// <param name="jwt">JWT to make the assertions on.</param>
        /// <param name="claimType">The claim type to assert.</param>
        /// <param name="claimValue">The claim value to assert. If null then claim value can be anything (it is not checked).</param>
        /// <param name="optional">If true then the claim itself is optional and doesn't need to exist in the claims.</param>
        static public void AssertClaim(this JwtSecurityToken jwt, string claimType, string? claimValue, bool optional = false)
        {
            var claims = jwt.Claims.Where(claim => claim.Type == claimType);

            // Claim not found and it's optional so just exit
            if (optional && !claims.Any())
            {
                return;
            }

            claims.Should().NotBeNull(claimType);
            claims.Should().ContainSingle(claimType);

            // Check value value
            if (claimValue != null)
            {
                var claim = claims.First();
                claim.Value.Should().Be(claimValue, claimType);
            }
        }
    }
}