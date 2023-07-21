using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     文件服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class FileController : ControllerBase<IFileService>, IFileModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FileController" /> class.
    /// </summary>
    public FileController(IFileService service) //
        : base(service) { }

    /// <summary>
    ///     文件上传
    /// </summary>
    public Task<string> UploadAsync(IFormFile file)
    {
        return Service.UploadAsync(file);
    }
}