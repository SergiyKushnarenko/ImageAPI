using ImageDAL.Models;
using ImageDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageDAL.Repositories;

public class FavoriteUserImageRepository : Repository<FavoriteUserImage>, IFavoriteUserImageRepository
{
    public FavoriteUserImageRepository(ImageDbContext context) : base(context)
    {
    }

    public async Task<FavoriteUserImage> GetAsync(int id)
    {
        return await GetAll().FirstOrDefaultAsync(ent => ent.Id == id);
    }
    public async Task<List<FavoriteUserImage>> GetAllByUserId(int id)
    {
        return await GetAll().Where(ent => ent.UserId == id).ToListAsync();
    }
}