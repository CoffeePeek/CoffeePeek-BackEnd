using Amazon.S3;
using Amazon.S3.Transfer;
using CoffeePeek.Photo.Application.Interfaces;
using CoffeePeek.Photo.Application.Services;
using CoffeePeek.Photo.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CoffeePeek.Photo.Infrastructure.Storage;

public class S3PhotoStorage : IPhotoStorage
{
    private readonly IAmazonS3 _s3Client;
    private readonly DigitalOceanOptions _options;

    public S3PhotoStorage(IOptions<DigitalOceanOptions> options)
    {
        _options = options.Value;

        var config = new AmazonS3Config
        {
            ServiceURL = _options.Endpoint,
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(_options.AccessKey, _options.SecretKey, config);
    }
    
    public async Task<string> UploadPhotoAsync(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        await using var stream = file.OpenReadStream();

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = fileName,
            BucketName = _options.PhotoSpaceName,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.PublicRead
        };

        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest);

        return $"{_options.Endpoint}/{_options.PhotoSpaceName}/{fileName}";
    }
}