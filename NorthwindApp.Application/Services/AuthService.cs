using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NorthwindApp.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NorthwindApp.Common;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

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

    public async Task<Result<CreateUserResponse>> RegisterUserAsync(CreateUserRequest request)
    {
        var newUser = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = DateTime.MinValue,
            RoleId = 1
        };
        
        var createdUser = await _userService.AddAsync(newUser, request.Password);

        return new CreateUserResponse(createdUser.FirstName, createdUser.LastName, createdUser.Email);
    }
    
    public async Task<Result<LoginResponse>> AuthenticateUserAsync(LoginRequest request)
    {
        var user = await _userService.GetByEmail(request.Email);

        if (user is null)
            return Result.Failure<LoginResponse>(AuthenticationErrors.UserNotFound);

        if (!_passwordHasher.VerifyPassword(user.PasswordHash, request.Password, user.PasswordSalt))
            return Result.Failure<LoginResponse>(AuthenticationErrors.IncorrectPassword);

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:SecretKey"];
        var tokenValidityMins = _configuration.GetValue<int>("Jwt:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Email, request.Email)
            ]),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), 
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponse(user.Email,
            accessToken,
            (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds);
    }
}
