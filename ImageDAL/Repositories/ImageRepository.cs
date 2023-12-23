using ImageDAL.Common.DTOs;
using ImageDAL.Models;
using ImageDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageDAL.Repositories;

public class ImageRepository : Repository<Image>, IImageRepository
{
    public ImageRepository(ImageDbContext context) : base(context)
    {
    }
    /// <summary>
    /// Get image by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Image> GetAsync(int id)
    {
        return await GetAll().FirstOrDefaultAsync(image => image.Id == id);
    }
    /// <summary>
    /// Get image by url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<Image> GetAsync(string url)
    {
        return await GetAll().FirstOrDefaultAsync(image => image.Url.Equals(url));
    }

    /// <summary>
    /// Get All Images with pagination
    /// </summary>
    /// <param name="filteringModel"></param>
    /// <returns></returns>
    public async Task<PaginationResultDto<Image>> GetAllImagesAsync(FilteringBaseDto filteringModel)
    {
        var query = GetAll();

        query = filteringModel.AscSort ? query.OrderBy(image => image.Id) : query.OrderByDescending(image => image.Id);

        var totalItems = await query.CountAsync();

        var images = await query
            .Skip((filteringModel.PageNumber - 1) * filteringModel.PageSize)
            .Take(filteringModel.PageSize)
            .ToListAsync();

        return new PaginationResultDto<Image>
        {
            PageNumber = filteringModel.PageNumber,
            PageData = images,
            TotalCount = totalItems,
            TotalPageCount = (int)Math.Ceiling((double)totalItems / filteringModel.PageSize),
        };
    }
}