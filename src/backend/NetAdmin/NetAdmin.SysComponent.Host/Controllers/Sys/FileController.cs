using NetAdmin.Domain.Dto.Sys.File;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     文件服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class FileController(IFileCache cache) : ControllerBase<IFileCache, IFileService>(cache), IFileModule
{
    /// <summary>
    ///     文件上传
    /// </summary>
    public Task<UploadFileRsp> UploadAsync(IFormFile file)
    {
        return Cache.UploadAsync(file);
    }
}