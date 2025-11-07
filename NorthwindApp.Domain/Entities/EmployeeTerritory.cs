namespace NorthwindApp.Domain;

public class EmployeeTerritory : BaseEntity
{
    public int EmployeeId { get; set; }
    public int TerritoryId { get; set; }
    
    public Employee? Employee { get; set; }

    public Territory? Territory { get; set; }
}