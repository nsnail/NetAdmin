using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

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
    public Task<string> UploadAsync(IFormFile file)
    {
        return Cache.UploadAsync(file);
    }
}