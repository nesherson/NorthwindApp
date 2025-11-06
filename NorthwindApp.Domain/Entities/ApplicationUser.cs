using Microsoft.AspNetCore.Identity;

namespace NorthwindApp.Domain;

public class ApplicationUser : IdentityUser<int>, IEntityDateInfo
{
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public DateTime? DateDeleted { get; set; }
    public UserProfile UserProfile { get; set; } = null!;
}