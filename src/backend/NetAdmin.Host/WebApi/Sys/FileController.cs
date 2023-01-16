using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     文件服务
/// </summary>
public class FileController : ControllerBase<IFileService>, IFileModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FileController" /> class.
    /// </summary>
    public FileController(IFileService service) //
        : base(service) { }

    /// <summary>
    ///     文件上传
    /// </summary>
    public async ValueTask<string> Upload(IFormFile file)
    {
        return await Service.Upload(file);
    }
}