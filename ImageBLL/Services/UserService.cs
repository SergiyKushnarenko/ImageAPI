using System.Data;
using System.Security.Claims;
using ImageBLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ImageBLL.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContext;

    public UserService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }
    public int UserId => GetUserId(_httpContext?.HttpContext?.User.Claims);
    public string Email => GetUserEmail(_httpContext?.HttpContext?.User.Claims);
    private int GetUserId(IEnumerable<Claim> claims)
    {
        var userId = claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToString() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return 0;
        }

        return int.Parse(userId);
    }

    private string GetUserEmail(IEnumerable<Claim> claims)
    {
        return claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
    }

    
}