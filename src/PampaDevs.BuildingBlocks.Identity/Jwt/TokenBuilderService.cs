using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PampaDevs.BuildingBlocks.Identity.Jwt
{
    public class TokenBuilderService
    {
        private readonly JwtSettings _settings;
        
        public TokenBuilderService(IOptions<JwtSettings> options)
        {
            _settings = options?.Value;
        }

        public string BuildToken(Action<JwtTokenBuilder> options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);

            var tokenBuilder = new JwtTokenBuilder();

            options?.Invoke(tokenBuilder);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Subject = CreateJwtClaims()
            });;

        }

        private ClaimsIdentity CreateJwtClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, ""),
                new Claim(JwtRegisteredClaimNames.Email, ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            };

            return new ClaimsIdentity(claims);
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            var timespan = (date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero));

            return (long)Math.Round(timespan.TotalSeconds);
        }
    }
}
