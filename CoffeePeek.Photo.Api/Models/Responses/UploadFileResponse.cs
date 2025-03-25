namespace CoffeePeek.Photo.Api.Models.Responses;

public class UploadFileResponse
{
    public UploadFileResponse(bool success, string photoUrl, string errorMessage)
    {
        Success = success;
        PhotoUrl = photoUrl;
        ErrorMessage = errorMessage;
    }

    public bool Success { get; }
    public string PhotoUrl { get; }
    public string ErrorMessage { get; }
}