namespace NorthwindApp.Models;

public record UpdateUserRequest(int Id, string FirstName, string LastName, string Email, string Password);
public record UpdateUserResponse(int Id, string FirstName, string LastName, string Email);
