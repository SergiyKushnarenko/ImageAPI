using ImageDAL.Repositories;
using ImageDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageDAL;

public static class Extensions
{
    public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ImageDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("ImageDB")));

        using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ImageDbContext>())
            {
                context.Database.Migrate();
            }
        }

        services.AddScoped(typeof(IImageRepository), typeof(ImageRepository));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IFavoriteUserImageRepository), typeof(FavoriteUserImageRepository));
    }
}
