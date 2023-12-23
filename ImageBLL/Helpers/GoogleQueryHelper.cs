using ImageDAL.Repositories.Interfaces;

namespace ImageBLL.Helpers;

public static class GoogleQueryHelper
{
    private const string ApiKey = "b2dd2f1bf9c03aa40649e806fedba69b3591abd3071d142a1291ab7a92489660";
    private const string SearchEngineId = "google_images";
   
    public static string CreateQuery(string query) => $"https://serpapi.com/search.json?q={query}&tbm=isch&api_key={ApiKey}&search_engine_id={SearchEngineId}";
}