using System.ComponentModel.DataAnnotations;

namespace NorthwindApp.WebApi;

public class UserCreateRestModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public string Password { get; set; }
}