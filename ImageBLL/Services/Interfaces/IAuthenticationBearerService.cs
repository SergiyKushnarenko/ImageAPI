using ImageBLL.Models.Auth;

namespace ImageBLL.Services.Interfaces;

public interface IAuthenticationBearerService
{
    Task<ResponseModelDto> Login(LoginDto loginDto);
    ResponseModelDto Register(CreateUserDto user);
}