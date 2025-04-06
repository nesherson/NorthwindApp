using NorthwindApp.Domain;

namespace NorthwindApp.WebApi.Extensions
{
    public static class UserMappingExtensions
    {
        public static void MapTo(this User entity, UserReadRestModel model)
        {
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.Email = entity.Email;
            model.DateOfBirth = entity.DateOfBirth;
            model.Role = new RoleReadRestModel
            {
                Abrv = entity.Role.Abrv,
                Name = entity.Role.Name
            };
            model.DateCreated = entity.DateCreated;
            model.DateModified = entity.DateModified;
            model.DateDeleted = entity.DateDeleted;
        }

        public static void MapTo(this UserCreateRestModel model, User entity)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.DateOfBirth = model.DateOfBirth;
            entity.RoleId = model.RoleId;
        }
    }
}