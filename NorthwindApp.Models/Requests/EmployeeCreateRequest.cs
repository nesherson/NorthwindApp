namespace NorthwindApp.Models;

public record EmployeeCreateRequest(string FirstName,
    string LastName,
    string Title,
    string TitleOfCourtesy,
    DateTime? BirthDate,
    DateTime? HireDate,
    string Address,
    string City,
    string Region,
    string PostalCode,
    string Country,
    string HomePhone,
    string Extension,
    string Notes,
    int? ReportsToId,
    byte[] Photo,
    string PhotoPath);