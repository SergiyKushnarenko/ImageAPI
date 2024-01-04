using ImageBLL.Models.Auth;
using ImageDAL.Models;

namespace ImageBLL.Services.Interfaces;

public interface IAuthenticationBearerService
{
    Task<ResponseModelDto> Login(LoginDto loginDto);
    ResponseModelDto Register(CreateUserDto user);
    Task<ResponseModelDto> LoginByGoogle(User currentUser);
}