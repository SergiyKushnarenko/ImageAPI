using ImageDLL.Enums;

namespace ImageDAL.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole UserRoleId { get; set; }
}