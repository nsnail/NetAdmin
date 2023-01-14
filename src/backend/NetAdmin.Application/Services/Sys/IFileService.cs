using Microsoft.AspNetCore.Http;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     文件服务
/// </summary>
public interface IFileService : IService
{
    /// <summary>
    ///     文件上传
    /// </summary>
    ValueTask<string> Upload(IFormFile file);
}