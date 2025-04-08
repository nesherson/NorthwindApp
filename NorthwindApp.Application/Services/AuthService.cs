using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NorthwindApp.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NorthwindApp.Application;

public class AuthService : IAuthService
{
    private readonly IUserService _userService; 
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    public AuthService(IUserService userService,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<LoginResult> LogInUser(string email, string password)
    {
        var user = await _userService.GetByEmail(email);

        if (user == null)
            return new LoginResult("User not found.", false);

        var passwordHash = _passwordHasher.ComputeHash(password, user.PasswordSalt);

        if (user.PasswordHash != passwordHash)
            return new LoginResult("Username or password did not match.", false);

        return new LoginResult("Login successfull", true, GeneretateJWT(user));
    }

    private string GeneretateJWT(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName}-{user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
