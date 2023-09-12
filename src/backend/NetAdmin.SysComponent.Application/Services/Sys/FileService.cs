using NetAdmin.Application.Services;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IFileService" />
public sealed class FileService : ServiceBase<IFileService>, IFileService
{
    private readonly MinioHelper   _minioHelper;
    private readonly UploadOptions _uploadOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="FileService" /> class.
    /// </summary>
    public FileService(IOptions<UploadOptions> uploadOptions, MinioHelper minioHelper) //
    {
        _minioHelper   = minioHelper;
        _uploadOptions = uploadOptions.Value;
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">文件不能为空</exception>
    /// <exception cref="NetAdminInvalidOperationException">允许上传的文件格式</exception>
    /// <exception cref="NetAdminInvalidOperationException">允许的文件大小</exception>
    public async Task<string> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length < 1) {
            throw new NetAdminInvalidOperationException(Ln.文件不能为空);
        }

        if (!_uploadOptions.ContentTypes.Contains(file.ContentType)) {
            throw new NetAdminInvalidOperationException(
                $"{Ln.允许的文件格式} {string.Join(",", _uploadOptions.ContentTypes)}");
        }

        if (file.Length > _uploadOptions.MaxSize) {
            throw new NetAdminInvalidOperationException($"{Ln.允许的文件大小} {_uploadOptions.MaxSize}");
        }

        var             fileName   = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var             objectName = $"{UserToken.Id}/{fileName}";
        await using var fs         = file.OpenReadStream();
        return await _minioHelper.UploadAsync(objectName, fs, file.ContentType, file.Length);
    }
}