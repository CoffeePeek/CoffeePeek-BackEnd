using Microsoft.AspNetCore.Http;

namespace CoffeePeek.Contract.Messages.Photos;

public class PhotoUploadMessage(int photoId, IFormFile file)
{
    public int PhotoId { get; } = photoId;
    public IFormFile File { get; } = file;
}