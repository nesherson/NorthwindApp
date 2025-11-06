namespace NorthwindApp.Domain;

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public int UserId { get; set; }
}