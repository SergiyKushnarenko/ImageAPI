using ImageBLL.Models.Favorite;
using ImageBLL.Services.Interfaces;
using ImageDAL.Common.DTOs;
using ImageDAL.Models;
using ImageDAL.Repositories;
using ImageDAL.Repositories.Interfaces;

namespace ImageBLL.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;

    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    /// <summary>
    /// Get all images By filtering params
    /// </summary>
    /// <param name="filteringModel"></param>
    /// <returns></returns>
    public async Task<PaginationResultDto<Image>> GetAllImages(FilteringBaseDto filteringModel)
    {
        var result = await _imageRepository.GetAllImagesAsync(filteringModel);
        return result;
    }
}