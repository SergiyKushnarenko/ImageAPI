using ImageDAL.Models;
using Newtonsoft.Json.Linq;

namespace ImageBLL.Services.Interfaces;

public interface IGoogleImageService
{
    Task<List<Image>> SearchImagesAsync(string query);
}