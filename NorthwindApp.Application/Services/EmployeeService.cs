using NorthwindApp.Application.Common.Constants;
using NorthwindApp.Application.Mapping;
using NorthwindApp.Common;
using NorthwindApp.Common.Collections;
using NorthwindApp.Infrastructure;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.Application;

public class EmployeeService : IEmployeeService
{
    private readonly NorthwindAppDbContext _dbContext;

    public EmployeeService(NorthwindAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<EmployeeResponse>> CreateEmployeeAsync(EmployeeCreateRequest request)
    {
        var newEmployee = request.ToEntity();

        _dbContext.Employees.Add(newEmployee);
        await _dbContext.SaveChangesAsync();

        return newEmployee.ToResponse();
    }

    public async Task<Result<EmployeeResponse>> UpdateEmployeeAsync(EmployeeUpdateRequest request)
    {
        var employeeToUpdate = await _dbContext.Employees
            .FindAsync(request.Id);

        if (employeeToUpdate is null)
            return Result.Failure<EmployeeResponse>(EmployeeErrors.EmployeeNotFound(request.Id));

        employeeToUpdate.ApplyUpdate(request);

        _dbContext.Employees.Update(employeeToUpdate);
        await _dbContext.SaveChangesAsync();

        return employeeToUpdate.ToResponse();
    }

    public async Task<Result> DeleteEmployeeAsync(int id)
    {
        var employeeToDelete = await _dbContext.Employees
            .FindAsync(id);

        if (employeeToDelete is null)
            return Result.Failure<EmployeeResponse>(EmployeeErrors.EmployeeNotFound(id));

        _dbContext.Employees.Remove(employeeToDelete);
        await _dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<EmployeeResponse>> GetEmployeeAsync(int id)
    {
        var employee = await _dbContext.Employees
            .FindAsync(id);

        if (employee is null)
            return Result.Failure<EmployeeResponse>(EmployeeErrors.EmployeeNotFound(id));

        return employee.ToResponse();
    }

    public async Task<Result<PaginatedList<EmployeeResponse>>> GetPaginatedEmployeesAsync(string? sortColumn = null,
        string sortOrder = "desc",
        int pageIndex = 1,
        int pageSize = 25)
    {
        var projection = _dbContext.Employees
            .AsQueryable()
            .OrderByDynamic(sortColumn ?? SortingConstants.DateCreated, sortOrder)
            .Select(EmployeeMappingExtensions.ToResponseProjection);

        var result = await PaginatedList<EmployeeResponse>.CreateAsync(projection, pageIndex, pageSize);

        return result;
    }
}
