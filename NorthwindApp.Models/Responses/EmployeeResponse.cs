namespace NorthwindApp.Models;

public record EmployeeResponse(
    int Id,
    string FirstName,
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
    DateTime DateCreated,
    DateTime? DateModified = null);
