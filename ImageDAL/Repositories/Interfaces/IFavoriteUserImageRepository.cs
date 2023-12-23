using ImageDAL.Models;
using ImageDAL.Repositories.Interrfaces;

namespace ImageDAL.Repositories.Interfaces;

public interface IFavoriteUserImageRepository : IRepository<FavoriteUserImage>
{
    Task<FavoriteUserImage> GetAsync(int id);
    Task<List<FavoriteUserImage>> GetAllByUserId(int id);
}