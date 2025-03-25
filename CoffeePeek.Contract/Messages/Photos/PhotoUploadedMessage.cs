namespace CoffeePeek.Contract.Messages.Photos;

public class PhotoUploadedMessage(string photoId, string url)
{
    public string PhotoId { get; set; } = photoId;
    public string Url { get; set; } = url;
}