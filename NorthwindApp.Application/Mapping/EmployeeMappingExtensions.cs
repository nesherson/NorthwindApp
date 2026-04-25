using System.Linq.Expressions;
using NorthwindApp.Domain;
using NorthwindApp.Models;

namespace NorthwindApp.Application.Mapping;

public static class EmployeeMappingExtensions
{
    public static Employee ToEntity(this EmployeeCreateRequest request)
    {
        return new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Title = request.Title,
            TitleOfCourtesy = request.TitleOfCourtesy,
            BirthDate = request.BirthDate,
            HireDate = request.HireDate,
            Address = request.Address,
            City = request.City,
            Region = request.Region,
            PostalCode = request.PostalCode,
            Country = request.Country,
            HomePhone = request.HomePhone,
            Extension = request.Extension,
            Notes = request.Notes,
            ReportsToId = request.ReportsToId,
            Photo =  request.Photo,
            PhotoPath = request.PhotoPath
        };
    }

    public static void ApplyUpdate(this Employee employee, EmployeeUpdateRequest request)
    {
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Title = request.Title;
        employee.TitleOfCourtesy = request.TitleOfCourtesy;
        employee.BirthDate = request.BirthDate;
        employee.HireDate = request.HireDate;
        employee.Address = request.Address;
        employee.City = request.City;
        employee.Region = request.Region;
        employee.PostalCode = request.PostalCode;
        employee.Country = request.Country;
        employee.HomePhone = request.HomePhone;
        employee.Extension = request.Extension;
        employee.Notes = request.Notes;
        employee.ReportsToId = request.ReportsToId;
    }

    public static EmployeeResponse ToResponse(this Employee employee)
    {
        return new EmployeeResponse(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Title,
            employee.TitleOfCourtesy,
            employee.BirthDate,
            employee.HireDate,
            employee.Address,
            employee.City,
            employee.Region,
            employee.PostalCode,
            employee.Country,
            employee.HomePhone,
            employee.Extension,
            employee.Notes,
            employee.ReportsToId,
            employee.DateCreated,
            employee.DateModified);
    }

    public static Expression<Func<Employee, EmployeeResponse>> ToResponseProjection { get; } =
        employee => new EmployeeResponse(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Title,
            employee.TitleOfCourtesy,
            employee.BirthDate,
            employee.HireDate,
            employee.Address,
            employee.City,
            employee.Region,
            employee.PostalCode,
            employee.Country,
            employee.HomePhone,
            employee.Extension,
            employee.Notes,
            employee.ReportsToId,
            employee.DateCreated,
            employee.DateModified);
}
