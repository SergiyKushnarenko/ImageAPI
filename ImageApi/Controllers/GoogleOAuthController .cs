
using ImageBLL.DTOs;
using ImageBLL.Helpers;
using ImageBLL.Models.Auth;
using ImageBLL.Services;
using ImageBLL.Services.Interfaces;
using ImageDAL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GoogleOAuthController : Controller
    {
        private const string PkceSessionKey = "codeVerifier";

        private readonly IUserService _userService;
        private readonly IGoogleOAuthService _googleOAuthService;

        public GoogleOAuthController(IUserService userService, IGoogleOAuthService googleOAuthService)
        {
            _userService = userService;
            _googleOAuthService = googleOAuthService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult RedirectOnOAuthServer()
        {
            var codeVerifier = Guid.NewGuid().ToString();
            var codeChallenge = Sha256Helper.ComputeHash(codeVerifier);

            HttpContext.Session.SetString(PkceSessionKey, codeVerifier);

            var googleAuthUrl = _googleOAuthService.GenerateOAuthRequestUrl(codeChallenge);
            return Ok(googleAuthUrl);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> CodeAsync([FromBody] CodeRequestModel model)
        {
            var code = model.Code;
            var codeVerifier = HttpContext.Session.GetString(PkceSessionKey);

            var jwtToken = await _googleOAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier);
            return Ok(jwtToken);
        }
        public class CodeRequestModel
        {
            public string Code { get; set; }
        }
    }
}
