using NorthwindApp.Common;

namespace NorthwindApp.Models.Errors;

public static class UserErrors
{
    public static Error CreateUserError()
        => Error.Problem("User.CreateUserError",
            $"User couldn't be created.");
    
    public static Error UserNotFound(int id)
        => Error.Problem("User.UserNotFound",
            $"User with Id: {id} does not exist.");
    
    public static Error UserNotFound(string email)
        => Error.Problem("User.UserNotFound",
            $"User with email: {email} does not exist.");
}

public static class RegionErrors
{
    public static Error RegionNotFound(int id)
        => Error.Problem("Region.RegionNotFound",
            $"Region with Id: {id} does not exist.");
}