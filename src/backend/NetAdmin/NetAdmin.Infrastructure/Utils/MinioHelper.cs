using Minio;
using Minio.DataModel.Args;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     Minio 帮助类
/// </summary>
public sealed class MinioHelper(IOptions<UploadOptions> uploadOptions) : IScoped
{
    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="objectName">对象名称</param>
    /// <param name="fileStream">文件流</param>
    /// <param name="contentType">文件类型</param>
    /// <param name="fileSize">文件大小</param>
    /// <returns>文件名,可访问的url地址</returns>
    public async Task<(string FileName, string Url)> UploadAsync(
        string objectName
        , Stream fileStream
        , string contentType
        , long fileSize
    ) {
        using var minio = new MinioClient()
            .WithEndpoint(uploadOptions.Value.Minio.ServerAddress)
            .WithCredentials(uploadOptions.Value.Minio.AccessKey, uploadOptions.Value.Minio.SecretKey)
            .WithSSL(uploadOptions.Value.Minio.Secure)
            .Build();

        var beArgs = new BucketExistsArgs().WithBucket(uploadOptions.Value.Minio.BucketName);

        if (!await minio.BucketExistsAsync(beArgs).ConfigureAwait(false)) {
            var mbArgs = new MakeBucketArgs().WithBucket(uploadOptions.Value.Minio.BucketName);
            await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
        }

        var putArgs = new PutObjectArgs()
            .WithBucket(uploadOptions.Value.Minio.BucketName)
            .WithObject(objectName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileSize)
            .WithContentType(contentType);
        _ = await minio.PutObjectAsync(putArgs).ConfigureAwait(false);

        return (objectName, $"{uploadOptions.Value.Minio.AccessUrl}/{uploadOptions.Value.Minio.BucketName}/{objectName}");
    }
}