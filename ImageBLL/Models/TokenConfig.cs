namespace ImageBLL.Models;

public class TokenConfig
{
    public DateTime ExpirationTime { get; set; }
    public Dictionary<string, string> Claims { get; set; }
}