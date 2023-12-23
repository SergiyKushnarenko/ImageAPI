using ImageDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageDAL;

public class ImageDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<FavoriteUserImage> FavoriteUserImages { get; set; } = null!;

    public ImageDbContext()
    {
    }

    public ImageDbContext(DbContextOptions<ImageDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}