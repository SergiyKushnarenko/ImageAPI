using System.ComponentModel.DataAnnotations;
using ImageDLL.Enums;

namespace ImageBLL.Models.Auth;

public class CreateUserDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "User role is required.")]
    public UserRole UserRoleId { get; set; }
}