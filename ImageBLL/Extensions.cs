using ImageBLL.Mapping;
using ImageBLL.Services;
using ImageBLL.Services.Interfaces;
using ImageDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ImageBLL;

public static class Extensions
{
    public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddScoped<IUserService, UserService>();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddDataLayer(configuration);

        services.AddScoped<IAuthenticationBearerService, AuthenticationBearerService>();
        services.AddScoped<IGoogleImageService, GoogleImageService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IGoogleOAuthService, GoogleOAuthService>();
    }
}