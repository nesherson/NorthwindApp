using NorthwindApp.Domain;

namespace NorthwindApp.WebApi.Extensions
{
    public static class UserMappingExtensions
    {
        public static void MapTo(this User user, UserReadRestModel model)
        {
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.DateOfBirth = user.DateOfBirth;
            model.Role = new RoleReadRestModel
            {
                Abrv = user.Role.Abrv,
                Name = user.Role.Name
            };
            model.DateCreated = user.DateCreated;
            model.DateModified = user.DateModified;
            model.DateDeleted = user.DateDeleted;
        }
    }
}