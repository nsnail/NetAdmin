using Microsoft.AspNetCore.Mvc;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     常量服务
/// </summary>
public interface IConstantService : IService
{
    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    object GetEnums();

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    object GetLocalizedStrings();

    /// <summary>
    ///     获得常量字符串
    /// </summary>
    IActionResult GetStrings();
}