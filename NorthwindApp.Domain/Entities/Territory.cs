namespace NorthwindApp.Domain;

public class Territory : BaseEntity
{
    public Territory()
    {
        EmployeeTerritories = new HashSet<EmployeeTerritory>();
    }
    
    public string Description { get; set; }
    public int RegionId { get; set; }
    
    public Region Region { get; set; }
    public ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
}