using ImageBLL.Models.Favorite;
using ImageBLL.Services;
using ImageBLL.Services.Interfaces;
using ImageDAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FavoriteImageDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToFavorite(int imageId)
        {
            try
            {
                var result = await _favoriteService.AddToFavorite(imageId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(FavoriteImageDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFromFavorite(int imageId)
        {
            try
            {
                var result = await _favoriteService.DeleteFromFavorite(imageId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get list of favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListFavorite")]
        [ProducesResponseType(typeof(List<FavoriteImageDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _favoriteService.GetAllFavorite();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
