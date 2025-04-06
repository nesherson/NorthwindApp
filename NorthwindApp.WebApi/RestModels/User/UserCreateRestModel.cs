namespace NorthwindApp.WebApi;

public class UserCreateRestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int RoleId { get; set; }
    public string Password { get; set; }
}