using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;
using WorkoutTracking.Application.ConfigurationTemplates;
using WorkoutTracking.Application.Services.Interfaces;

namespace WorkoutTracking.Application.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfiguration jwtConfiguration;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public JwtService(IOptions<JwtConfiguration> jwtConfiguration)
        {
            this.jwtConfiguration = jwtConfiguration.Value;
            tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SigningCredentials credentials = 
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key)),
                    SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    jwtConfiguration.Issuer,
                    jwtConfiguration.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddDays(jwtConfiguration.LifetimeDays),
                    signingCredentials: credentials);

            return tokenHandler.WriteToken(token);
        }
    }
}
