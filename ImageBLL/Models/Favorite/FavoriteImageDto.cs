namespace ImageBLL.Models.Favorite;

public class FavoriteImageDto
{
    public int Id { get; set; }
    public int ImageId { get; set; }
    public string Url { get; set; }
    public string SourceUrl { get; set; }
    public string Title { get; set; }
}