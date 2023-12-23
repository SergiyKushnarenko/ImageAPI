using ImageDLL.Enums;

namespace ImageBLL.Models.Auth;

public class ResponseModelDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JwtBearerToken { get; set; }
    public UserRole UserRoleId { get; set; }
}