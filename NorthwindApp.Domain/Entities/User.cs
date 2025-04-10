﻿namespace NorthwindApp.Domain;

public class User : BaseEntity, IEntityDateInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public DateTime? DateDeleted { get; set; }
}