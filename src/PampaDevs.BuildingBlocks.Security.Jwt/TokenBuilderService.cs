using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PampaDevs.BuildingBlocks.Security.Jwt
{
    public class TokenBuilderService
    {
        private readonly JwtSettings _settings;
        
        public TokenBuilderService(IOptions<JwtSettings> options)
        {
            _settings = options?.Value;
        }

        public string BuildToken(Action<JwtTokenBuilder<IdentityUser>> options)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecretKey));

            var tokenBuilder = new JwtTokenBuilder();

            options?.Invoke(tokenBuilder);
            
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Subject = tokenBuilder.IdentityClaims,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow, 
                Expires = DateTime.UtcNow.AddHours(_settings.Expiration),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            });;

            return tokenHandler.WriteToken(token);
        }
    }
}
