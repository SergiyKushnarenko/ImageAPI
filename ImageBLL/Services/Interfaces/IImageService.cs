using ImageDAL.Common.DTOs;
using ImageDAL.Models;

namespace ImageBLL.Services.Interfaces;

public interface IImageService
{
    Task<PaginationResultDto<Image>> GetAllImages(FilteringBaseDto filteringModel);
}