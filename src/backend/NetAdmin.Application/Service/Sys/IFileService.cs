using Microsoft.AspNetCore.Http;

namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     文件服务
/// </summary>
public interface IFileService : IService
{
    /// <summary>
    ///     文件上传
    /// </summary>
    Task<string> Upload(IFormFile file);
}