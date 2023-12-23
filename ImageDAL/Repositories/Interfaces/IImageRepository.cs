using ImageDAL.Common.DTOs;
using ImageDAL.Models;
using ImageDAL.Repositories.Interrfaces;
using System;
namespace ImageDAL.Repositories.Interfaces;

public interface IImageRepository : IRepository<Image>
{
    Task<Image> GetAsync(int id);
    Task<Image> GetAsync(string ulr);
    Task<PaginationResultDto<Image>> GetAllImagesAsync(FilteringBaseDto filteringModel);
}