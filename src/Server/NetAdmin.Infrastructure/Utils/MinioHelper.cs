using Minio;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     MinioHelper
/// </summary>
public class MinioHelper : IScoped
{
    private readonly UploadOptions _uploadOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MinioHelper" /> class.
    /// </summary>
    public MinioHelper(IOptions<UploadOptions> uploadOptions)
    {
        _uploadOptions = uploadOptions.Value;
    }

    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="objectName">对象名称</param>
    /// <param name="fileStream">文件流</param>
    /// <param name="contentType">文件类型</param>
    /// <param name="fileSize">文件大小</param>
    /// <returns>可访问的url地址</returns>
    public async Task<string> UploadAsync(string objectName, Stream fileStream, string contentType, long fileSize)
    {
        using var minio = new MinioClient().WithEndpoint(_uploadOptions.Minio.ServerAddress)
                                           .WithCredentials( //
                                               _uploadOptions.Minio.AccessKey, _uploadOptions.Minio.SecretKey)
                                           .WithSSL(_uploadOptions.Minio.Secure)
                                           .Build();

        var beArgs = new BucketExistsArgs().WithBucket(_uploadOptions.Minio.BucketName);

        if (!await minio.BucketExistsAsync(beArgs)) {
            var mbArgs = new MakeBucketArgs().WithBucket(_uploadOptions.Minio.BucketName);
            await minio.MakeBucketAsync(mbArgs);
        }

        var putArgs = new PutObjectArgs().WithBucket(_uploadOptions.Minio.BucketName)
                                         .WithObject(objectName)
                                         .WithStreamData(fileStream)
                                         .WithObjectSize(fileSize)
                                         .WithContentType(contentType);
        await minio.PutObjectAsync(putArgs);

        return $"{_uploadOptions.Minio.AccessUrl}/{_uploadOptions.Minio.BucketName}/{objectName}";
    }
}