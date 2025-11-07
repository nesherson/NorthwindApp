namespace NorthwindApp.Domain;

public class Region : BaseEntity
{
    public Region()
    {
        Territories = new HashSet<Territory>();
    }
    
    public string Description { get; set; }
    
    public ICollection<Territory> Territories { get; set; }
}