using System.Runtime.CompilerServices;
using AutoMapper;
using ImageBLL.Models.Favorite;
using ImageBLL.Services.Interfaces;
using ImageDAL.Models;
using ImageDAL.Repositories.Interfaces;

namespace ImageBLL.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteUserImageRepository _favoriteUserImageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public FavoriteService(IFavoriteUserImageRepository favoriteUserImageRepository, IUserRepository userRepository, IImageRepository imageRepository, IUserService userService, IMapper mapper)
    {
        _favoriteUserImageRepository = favoriteUserImageRepository;
        _userRepository = userRepository;
        _imageRepository = imageRepository;
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Add Image to Favorite
    /// </summary>
    /// <param name="imageId"></param>
    /// <returns></returns>
    public async Task<FavoriteImageDto> AddToFavorite(int imageId)
    {
        var currentImage = await _imageRepository.GetAsync(imageId);
        var userId = _userService.UserId;
        var random = new Random();
        var entity = new FavoriteUserImage()
        {
            Id = random.Next(),
            ImageId = currentImage.Id,
            UserId = userId,
        };
        var result = await _favoriteUserImageRepository.CreateAsync(entity);
        var mappedResult = _mapper.Map<FavoriteImageDto>(currentImage);
        mappedResult.Id = result.Id;
        return mappedResult;
    }

    /// <summary>
    /// Delete Image from Favorite
    /// </summary>
    /// <param name="imageId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<FavoriteImageDto> DeleteFromFavorite(int imageId)
    {
        var currentFavoriteImage = await _favoriteUserImageRepository.GetAsync(imageId);
        if (currentFavoriteImage is null)
            throw new Exception($"FavoriteImage with Id: '{imageId}' was not found");
        var result = await _favoriteUserImageRepository.DeleteAsync(currentFavoriteImage);
        var currentImage = await _imageRepository.GetAsync(result.ImageId);
        var mappedResult = _mapper.Map<FavoriteImageDto>(currentImage);
        mappedResult.Id = result.Id;
        return mappedResult;
    }

    /// <summary>
    /// Get List of Favorite Images
    /// </summary>
    /// <returns></returns>
    public async Task<List<FavoriteImageDto>> GetAllFavorite()
    {
        var result = new List<FavoriteImageDto>();
        var currentImage = await _favoriteUserImageRepository.GetAllByUserId(_userService.UserId);
        foreach (var favoriteUserImage in currentImage)
        {
            var currentItem = await _imageRepository.GetAsync(favoriteUserImage.ImageId);
            var mappedResult = _mapper.Map<FavoriteImageDto>(currentItem);
            mappedResult.Id = favoriteUserImage.Id;
            result.Add(mappedResult);
        }
        return result;
    }
}