using ImageBLL.Models.Favorite;
using ImageDAL.Models;

namespace ImageBLL.Services.Interfaces;

public interface IFavoriteService
{
    Task<FavoriteImageDto> AddToFavorite(int imageId);
    Task<FavoriteImageDto> DeleteFromFavorite(int imageId);
    Task<List<FavoriteImageDto>> GetAllFavorite();
}