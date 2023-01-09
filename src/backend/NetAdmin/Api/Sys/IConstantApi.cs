using Microsoft.AspNetCore.Mvc;

namespace NetAdmin.Api.Sys;

/// <summary>
///     常量接口
/// </summary>
public interface IConstantApi
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