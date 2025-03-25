using CoffeePeek.Photo.Api.Models.Requests;
using CoffeePeek.Photo.Api.Models.Responses;
using CoffeePeek.Photo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePeek.Photo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController(IPhotoUploadService photoUploadService) : Controller
{
    [HttpPost("upload")]
    public async Task<UploadFileResponse> UploadPhoto(UploadFileRequest request)
    {
        var result = await photoUploadService.UploadPhotoAsync(request.File);

        var response = new UploadFileResponse(true, result, "null");

        return response;
    }
}