namespace CoffeePeek.Photo.Api.Models.Requests;

public class UploadFileRequest
{
    public int PhotoId { get; set; }
    public IFormFile File { get; set; }
}