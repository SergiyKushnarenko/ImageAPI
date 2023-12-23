using ImageBLL.Models.Favorite;
using ImageBLL.Services.Interfaces;
using ImageDAL.Common.DTOs;
using ImageDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImageController : ControllerBase
    {

        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Get all images
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<Image>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListImages(FilteringBaseDto filteringModel)
        {
            try
            {
                var result = await _imageService.GetAllImages(filteringModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
