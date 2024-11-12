namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     文件模块
/// </summary>
public interface IFileModule
{
    /// <summary>
    ///     文件上传
    /// </summary>
    Task<string> UploadAsync(IFormFile file);
}