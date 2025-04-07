namespace CoffeePeek.Photo.Core.Interfaces;

public interface IPhotoStorage
{
    Task<string> UploadPhotoAsync(byte[] file, string keyName);
}