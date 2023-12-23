namespace ImageBLL.Models.Auth;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
}