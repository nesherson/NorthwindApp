using NorthwindApp.Common;

namespace NorthwindApp.Models.Errors;

public static class EmployeeErrors
{
    public static Error EmployeeNotFound(int id)
        => Error.Problem("Employee.EmployeeNotFound",
            $"Employee with Id: {id} does not exist.");
}
