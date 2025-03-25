using Microsoft.AspNetCore.Http;

namespace CoffeePeek.Photo.Application.Interfaces;

public interface IPhotoStorage
{
    Task<string> UploadPhotoAsync(IFormFile file);
}