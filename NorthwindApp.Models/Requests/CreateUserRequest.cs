namespace NorthwindApp.Models;

public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);