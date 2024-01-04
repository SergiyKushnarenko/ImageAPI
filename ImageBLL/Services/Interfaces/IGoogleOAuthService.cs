using ImageBLL.Models.Auth;

namespace ImageBLL.Services.Interfaces;

public interface IGoogleOAuthService
{
    string GenerateOAuthRequestUrl(string codeChallenge);
    Task<ResponseModelDto> ExchangeCodeOnTokenAsync(string code, string codeVerifier);
}