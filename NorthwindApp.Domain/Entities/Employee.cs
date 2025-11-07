namespace NorthwindApp.Domain;

public class Employee : BaseEntity
{
    public Employee()
    {
        EmployeeTerritories = new HashSet<EmployeeTerritory>();
        Subordinates = new HashSet<Employee>();
    }

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Title { get; set; }
    public string TitleOfCourtesy { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string HomePhone { get; set; }
    public string Extension { get; set; }
    public byte[] Photo { get; set; }
    public string Notes { get; set; }
    public int? ReportsToId { get; set; }
    public string PhotoPath { get; set; }
    
    public Employee? ReportsTo { get; set; }
    public ICollection<Employee> Subordinates { get; set; }
    public ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }
}