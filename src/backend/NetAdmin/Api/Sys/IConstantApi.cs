using Microsoft.AspNetCore.Mvc;

namespace NetAdmin.Api.Sys;

/// <summary>
///     常量接口
/// </summary>
public interface IConstantApi
{
    /// <summary>
    ///     获得枚举常量
    /// </summary>
    object GetEnums();

    /// <summary>
    ///     获得字符串常量
    /// </summary>
    IActionResult GetStrings();
}