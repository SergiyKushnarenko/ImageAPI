namespace ImageDAL.Models;

public class FavoriteUserImage
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ImageId { get; set; }
    public Image Image { get; set; }
}