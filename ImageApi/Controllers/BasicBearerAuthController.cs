using ImageBLL.Models.Auth;
using ImageBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BasicBearerAuthController : ControllerBase
    {
        private readonly IAuthenticationBearerService _authenticationService;

        public BasicBearerAuthController(IAuthenticationBearerService authenticationServiceService)
        {
            _authenticationService = authenticationServiceService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModelDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authenticationService.Login(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseModelDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            var result = _authenticationService.Register(userDto);
            return Ok(result);
        }
    }
}
