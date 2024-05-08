using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce.service;

public class AuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
        Console.WriteLine($"{_configuration["Jwt:Issuer"]}");
    }

    public string GenerateJwt(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    //Role-based
                    new Claim(ClaimTypes.Role, user.Role == Role.Admin ? "Admin" : "User"),
                    //Claim-based
                    new Claim("IsBanned", user.IsBanned ? "true" : "false"),
                }
            ),

            Expires = DateTime.UtcNow.AddMinutes(45),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),

            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
