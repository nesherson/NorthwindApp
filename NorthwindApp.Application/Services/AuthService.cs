using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Infrastructure;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.Application;

public class AuthService : IAuthService
{
    private readonly NorthwindAppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(NorthwindAppDbContext dbContext,
        IConfiguration configuration,
        UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<Result> RegisterUserAsync(CreateUserRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
            return Result.Failure<CreateUserResponse>(AuthenticationErrors.IncorrectPassword);
        
        await _userManager.AddClaimsAsync(user, [
            new Claim(ClaimTypes.Name, request.FirstName),
            new Claim(ClaimTypes.Surname, request.LastName),
            new Claim(ClaimTypes.NameIdentifier, request.Email),
            new Claim(ClaimTypes.Email, request.Email),
        ]);
        
        return Result.Success();
    }

    public async Task<Result<AuthResponse>> LoginUserAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
            return Result.Failure<AuthResponse>(AuthenticationErrors.UserNotFound);
        
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return Result.Failure<AuthResponse>(AuthenticationErrors.IncorrectPassword);
        
        var (accessToken, accessTokenExpiration) = GenerateAccessToken(user.Id, user.Email!);
        var refreshToken = GenerateRefreshToken();
        
        await _userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", refreshToken);

        var userResponse = new UserTokenResponse(user.Id, user.Email!);
            
        return new AuthResponse
        {
            User = userResponse,
            AccessToken = accessToken, 
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = refreshToken
        };
    }

    public async Task<Result> LogoutUserAsync(ClaimsPrincipal user)
    {
        var identityUser = await _userManager.GetUserAsync(user);
        
        if (identityUser is null)
            return Result.Failure<AuthResponse>(AuthenticationErrors.UserNotFound);

        await _userManager.RemoveAuthenticationTokenAsync(identityUser, "Default", "RefreshToken");
        
        return Result.Success();
    }

    public async Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        
        if (principal?.Identity?.Name == null)
        {
            return Result.Failure<AuthResponse>(AuthenticationErrors.InvalidAccessToken);
        }

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);
        
        if (user == null)
        {
            return Result.Failure<AuthResponse>(AuthenticationErrors.InvalidAccessToken);
        }
        
        var savedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "Default", "RefreshToken");
        
        if (savedRefreshToken != request.RefreshToken)
        {
            return Result.Failure<AuthResponse>(AuthenticationErrors.InvalidRefreshToken);
        }
        
        var (newAccessToken, expiration) = GenerateAccessToken(user.Id, user.Email!);
        var newRefreshToken = GenerateRefreshToken();
        
        await _userManager.SetAuthenticationTokenAsync(user, "Default", "RefreshToken", newRefreshToken);
        
        var userResponse = new UserTokenResponse(user.Id, user.Email!);
            
        return new AuthResponse
        {
            User = userResponse,
            AccessToken = newAccessToken, 
            AccessTokenExpiration = expiration,
            RefreshToken = newRefreshToken
        };
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
            ValidateLifetime = false, // We don't care if it's expired
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                return null; // Invalid algorithm
            }
            return principal;
        }
        catch (Exception)
        {
            return null; // Token validation failed
        }
    }
    
    
    public (string, DateTime) GenerateAccessToken(string userId, string email)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:SecretKey"];
        var tokenValidityMins = _configuration.GetValue<int>("Jwt:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email)
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

        return (accessToken, tokenExpiryTimeStamp);
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
