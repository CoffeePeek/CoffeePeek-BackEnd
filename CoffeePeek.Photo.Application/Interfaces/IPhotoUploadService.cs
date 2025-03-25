using Microsoft.AspNetCore.Http;

namespace CoffeePeek.Photo.Application.Interfaces;

public interface IPhotoUploadService
{
    Task<string> UploadPhotoAsync(IFormFile requestFile);
}