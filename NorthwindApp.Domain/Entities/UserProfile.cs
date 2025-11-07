namespace NorthwindApp.Domain;

public class UserProfile : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public int UserId { get; set; }
}