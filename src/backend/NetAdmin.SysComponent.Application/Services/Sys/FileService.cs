using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IFileService" />
public sealed class FileService
    (IOptions<UploadOptions> uploadOptions, MinioHelper minioHelper) : ServiceBase<IFileService>, IFileService
{
    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">文件不能为空</exception>
    /// <exception cref="NetAdminInvalidOperationException">允许上传的文件格式</exception>
    /// <exception cref="NetAdminInvalidOperationException">允许的文件大小</exception>
    public async Task<string> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length < 1) {
            throw new NetAdminInvalidOperationException(Ln.文件不能为空);
        }

        if (!uploadOptions.Value.ContentTypes.Contains(file.ContentType)) {
            throw new NetAdminInvalidOperationException(
                $"{Ln.允许的文件格式} {string.Join(",", uploadOptions.Value.ContentTypes)}");
        }

        if (file.Length > uploadOptions.Value.MaxSize) {
            throw new NetAdminInvalidOperationException($"{Ln.允许的文件大小} {uploadOptions.Value.MaxSize}");
        }

        var             fileName   = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var             objectName = $"{UserToken.Id}/{fileName}";
        await using var fs         = file.OpenReadStream();
        return await minioHelper.UploadAsync(objectName, fs, file.ContentType, file.Length);
    }
}