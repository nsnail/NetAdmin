namespace NetAdmin.Api.Sys;

/// <summary>
///     文件接口
/// </summary>
public interface IFileApi : IRestfulApi
{
    /// <summary>
    ///     文件上传
    /// </summary>
    Task<string> Upload(IFormFile file);
}