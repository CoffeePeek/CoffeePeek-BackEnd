namespace CoffeePeek.Photo.Infrastructure.Options;

public class DigitalOceanOptions
{
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string PhotoSpaceName { get; set; }
    public string VideosSpaceName { get; set; }
    public string BackUpSpaceName { get; set; }
    public string Region { get; set; }
    public string Endpoint { get; set; }
}