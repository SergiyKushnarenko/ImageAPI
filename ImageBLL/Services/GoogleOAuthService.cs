using ImageBLL.DTOs;
using ImageBLL.Helpers;
using ImageBLL.Models.Auth;
using ImageBLL.Services.Interfaces;
using ImageDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace ImageBLL.Services
{
    public class GoogleOAuthService : IGoogleOAuthService
    {
        private const string RedirectUrl = "http://localhost:5173/login";
        private const string PkceSessionKey = "codeVerifier";
        private const string ClientId = "476147450115-qlu3k383q78b58gta68ho3i8mjstfbkt.apps.googleusercontent.com";
        private const string ClientSecret = "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";

        private readonly IAuthenticationBearerService _authenticationBearerService;

        public GoogleOAuthService(IAuthenticationBearerService authenticationBearerService)
        {
            _authenticationBearerService = authenticationBearerService;
        }

        public string GenerateOAuthRequestUrl(string codeChallenge)
        {
            // Создание URL для редиректа
            var query = new QueryBuilder {
            { "client_id", ClientId },
            { "redirect_uri", RedirectUrl },
            { "response_type", "code" },
            { "scope", "openid email profile" },
            { "code_challenge", codeChallenge },
            { "code_challenge_method", "S256" }
            }.ToString();

            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth{query}";
            return googleAuthUrl;
        }
        public async Task<ResponseModelDto> ExchangeCodeOnTokenAsync(string code, string codeVerifier)
        {
            var tokenRequest = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "code", code },
                { "code_verifier", codeVerifier },
                { "redirect_uri", RedirectUrl },
                { "grant_type", "authorization_code" }
            };

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(tokenRequest));

            if (!response.IsSuccessStatusCode)
            {
                // Логирование содержимого ошибочного ответа
                var errorContent = await response.Content.ReadAsStringAsync();
                // Выводите errorContent в журнал ошибок или возвращайте его в ответе
               throw new Exception($"Ошибка при обмене кода на токен: {errorContent}");
            }

            var tokenResponse = JsonConvert.DeserializeObject<TokenResultDto>(await response.Content.ReadAsStringAsync());
            var userInfo = await GetGoogleUserInfo(tokenResponse.AccessToken);
            var userName = userInfo.Name.Split(' ');
            var user = new User()
            {
                Email = userInfo.Email,
                FirstName = userName[0],
                LastName = userName[1]
            };
            var jwtToken = await  _authenticationBearerService.LoginByGoogle(user);
            return jwtToken;
        }

        //public  async Task<TokenResult> RefreshTokenAsync(string refreshToken)
        //{
        //    var refreshParams = new Dictionary<string, string>
        //{
        //    { "client_id", ClientId },
        //    { "client_secret", ClientSecret },
        //    { "grant_type", "refresh_token" },
        //    { "refresh_token", refreshToken }
        //};

        //    var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(TokenServerEndpoint, refreshParams);

        //    return tokenResult;
        //}

        private async Task<GoogleUserInfo> GetGoogleUserInfo(string accessToken)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Не удалось получить информацию о пользователе от Google.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(content);

            return userInfo;
        }
    }
}