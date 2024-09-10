using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce.service
{
    public class AuthService
    {
        public AuthService() { }

        public string GenerateJwt(User user)
        {
            var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key") 
                         ?? throw new InvalidOperationException("JWT Key is missing in environment variables.");
            var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer") 
                            ?? throw new InvalidOperationException("JWT Issuer is missing in environment variables.");
            var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience") 
                              ?? throw new InvalidOperationException("JWT Audience is missing in environment variables.");
            
            var key = Encoding.ASCII.GetBytes(jwtKey);
            
            // Ensure the key length is sufficient for security
            if (key.Length < 32)
                throw new InvalidOperationException("JWT Key is too short. It must be at least 32 characters.");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId?.ToString() ?? "Unknown"),
                    new Claim(ClaimTypes.Role, user.Role == Role.Admin ? "Admin" : "User"),
                    new Claim("IsBanned", user.IsBanned ? "true" : "false"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtIssuer,
                Audience = jwtAudience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}