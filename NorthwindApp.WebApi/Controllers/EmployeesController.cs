using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Common;
using NorthwindApp.Models;

namespace NorthwindApp.WebApi;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IResult> GetPaginated([FromQuery] string? sortColumn = null,
        [FromQuery] string sortOrder = "desc",
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 25)
    {
        var result = await _employeeService.GetPaginatedEmployeesAsync(sortColumn, sortOrder, pageIndex, pageSize);

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> Get(int id)
    {
        var result = await _employeeService.GetEmployeeAsync(id);

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpPost]
    public async Task<IResult> Create(EmployeeCreateRequest request)
    {
        var result = await _employeeService.CreateEmployeeAsync(request);

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpPut]
    public async Task<IResult> Update(EmployeeUpdateRequest request)
    {
        var result = await _employeeService.UpdateEmployeeAsync(request);

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> Delete(int id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);

        return result.Match(() => Results.Ok(), ApiResults.Problem);
    }
}
