using Amazon.S3;
using Amazon.S3.Transfer;
using CoffeePeek.Photo.Core.Interfaces;
using CoffeePeek.Photo.Infrastructure.Constants;
using CoffeePeek.Photo.Infrastructure.Options;
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
    
    public async Task<string> UploadPhotoAsync(byte[] photoBytes, string keyName)
    {
        await using var stream = new MemoryStream(photoBytes);

        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = keyName,
            BucketName = _options.PhotoSpaceName,
            ContentType = ContentType.ImageContentType,
            CannedACL = S3CannedACL.PublicRead
        };

        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest);

        return $"{_options.Endpoint}/{_options.PhotoSpaceName}/{keyName}";
    }
}