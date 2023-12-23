using ImageBLL.Models.Auth;
using ImageBLL.Services.Interfaces;
using ImageDAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ImageApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GoogleLoadImagesController : ControllerBase
    {
       private readonly IGoogleImageService _googleImageService;

       public GoogleLoadImagesController(IGoogleImageService googleImageService)
       {
           _googleImageService = googleImageService;
       }

       [HttpPost]
       [ProducesResponseType(typeof(Image), StatusCodes.Status200OK)]
       [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LoadImagesByQuery(string query)
        {
            try
            {
               var result = await _googleImageService.SearchImagesAsync(query);
               return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
