using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ecommerce_dash_api.Utils
{
    public class JwtToken
    {
        private readonly IConfiguration _configuration;

        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtTokenAsync(string email, List<string> roles, List<string> permissions)

        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
            var expirationInMinutes = Convert.ToInt32(jwtSettings["ExpirationInMinutes"]);

            var claims = new List<Claim>
            {
                new Claim("username", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(Constants.PermissionClaimType, permission)); 
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var generateToken = tokenHandler.WriteToken(token);

            return await Task.FromResult(generateToken);
        }
    }
}
