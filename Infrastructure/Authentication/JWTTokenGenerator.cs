using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Domain.UserAggregate;

namespace Infrastructure.Authentication
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        private readonly JWTSettings jwtSettings;
        public JWTTokenGenerator(IOptions<JWTSettings> JWTOptions)
        {
            jwtSettings = JWTOptions.Value;
        }
        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)
                ),
                SecurityAlgorithms.HmacSha256
            ); 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires:DateTime.UtcNow.AddDays(jwtSettings.ExpiryDays),
                claims: claims, 
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
