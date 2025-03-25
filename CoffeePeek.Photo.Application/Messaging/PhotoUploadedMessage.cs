namespace CoffeePeek.Photo.Application.Messaging;

public class PhotoUploadedMessage
{
    public int PhotoId { get; set; }
    public string PhotoUrl { get; set; }
}