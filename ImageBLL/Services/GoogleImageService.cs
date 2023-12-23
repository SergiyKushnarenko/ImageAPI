using System.Collections;
using ImageBLL.Helpers;
using ImageBLL.Services.Interfaces;
using ImageDAL.Models;
using ImageDAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;

namespace ImageBLL.Services;

public class GoogleImageService : IGoogleImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IHttpClientFactory _clientFactory;

    
    public GoogleImageService(IImageRepository imageRepository, IHttpClientFactory clientFactory)
    {
        _imageRepository = imageRepository;
        _clientFactory = clientFactory;
    }

    /// <summary>
    /// using serpApi for searching images
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<List<Image>> SearchImagesAsync(string query)
    {
        var client = _clientFactory.CreateClient();
        var url = GoogleQueryHelper.CreateQuery(query);
        var response = await client.GetStringAsync(url);
        var parseResult = ParseImages(JObject.Parse(response));
        if (parseResult.Any())
        {
            var result = await _imageRepository.CreateAsync(parseResult);
            //Need add mapper
            return result.ToList();
        }
        //Need add mapper
        return parseResult;

    }



    private List<Image> ParseImages(JObject jsonResponse)
    {
        var images = new List<Image>();

        foreach (var item in jsonResponse["images_results"])
        {
            images.Add(new Image
            {
                Url = item["thumbnail"]!.ToString(),
                SourceUrl = item["link"]!.ToString(),
                Title = item["title"]!.ToString(),
            });
        }

        return images;
    }
}