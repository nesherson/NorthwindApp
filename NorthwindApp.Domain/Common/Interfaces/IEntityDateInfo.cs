namespace NorthwindApp.Domain;

public interface IEntityDateInfo
{
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public DateTime? DateDeleted { get; set; }
}