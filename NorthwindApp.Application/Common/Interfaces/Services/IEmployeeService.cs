using NorthwindApp.Common;
using NorthwindApp.Common.Collections;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IEmployeeService
{
    Task<Result<EmployeeResponse>> CreateEmployeeAsync(EmployeeCreateRequest request);
    Task<Result<EmployeeResponse>> UpdateEmployeeAsync(EmployeeUpdateRequest request);
    Task<Result> DeleteEmployeeAsync(int id);
    Task<Result<EmployeeResponse>> GetEmployeeAsync(int id);
    Task<Result<PaginatedList<EmployeeResponse>>> GetPaginatedEmployeesAsync(string? sortColumn = null, string sortOrder = "desc", int pageIndex = 1, int pageSize = 25);
}
