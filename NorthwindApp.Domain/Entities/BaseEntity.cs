namespace NorthwindApp.Domain;

public abstract class BaseEntity : IEntityDateInfo
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public DateTime? DateDeleted { get; set; }
}