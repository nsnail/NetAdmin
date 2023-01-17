using Furion;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NSExt.Extensions;

namespace NetAdmin.Host.Aop.Filters;

/// <summary>
///     Api结果格式化处理器
///     协议：
///     当接口成功执行： result statusCode = 0， http statusCode = 2xx，
///     当接口异常：
///     a） 参数错误：
///     result statusCode = 4010-401x， http statusCode = 400，
///     b） 操作错误（业务异常）：
///     result statusCode = 4020-402x， http statusCode = 400，
///     c） 权限错误：
///     result statusCode = 4030-403x， http statusCode = 401，403
///     d） 安全保护：
///     result statusCode = 4040-404x， http statusCode = 400
///     e）未处理的异常：
///     result statusCode = 4000， http statusCode = 400
///     f) webserver级错误：
///     底层错误，未进入应用程序管道 （无封装），无errorCode， http statusCode = 404 （not found） ，405 （not allowed） ，502 （bad gateway）等等，
///     一般不会与本程序处理过的异常重合。
/// </summary>
[SuppressSniffer]
[UnifyModel(typeof(RestfulInfo<>))]
public class ApiResultHandler : IUnifyResultProvider
{
    /// <summary>
    ///     业务异常
    /// </summary>
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        var errorCodes = metadata.OriginErrorCode is Enums.StatusCodes code ? code : Enums.StatusCodes.Unexpected;
        var result     = RestfulResult(errorCodes, metadata.Data, metadata.Errors);

        return new JsonResult(result) { StatusCode = StatusCodes.Status400BadRequest };
    }

    /// <summary>
    ///     特定状态码返回值
    /// </summary>
    public async Task OnResponseStatusCodes( //
        HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
    {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

        var jsonOptions = App.GetOptions<JsonOptions>();
        switch (statusCode) {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync( //
                    RestfulResult(Enums.StatusCodes.IdentityMissing, null, Enums.StatusCodes.IdentityMissing.Desc())
                  , jsonOptions?.JsonSerializerOptions);
                break;

            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync( //
                    RestfulResult(Enums.StatusCodes.NoPermissions, null, Enums.StatusCodes.NoPermissions.Desc())
                  , jsonOptions?.JsonSerializerOptions);
                break;
        }
    }

    /// <summary>
    ///     成功返回值
    /// </summary>
    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        return new JsonResult(RestfulResult(0, data));
    }

    /// <summary>
    ///     验证失败
    /// </summary>
    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        return new JsonResult(RestfulResult(Enums.StatusCodes.InvalidInput, metadata.Data, metadata.ValidationResult)) {
                   StatusCode = StatusCodes.Status400BadRequest
               };
    }

    /// <summary>
    ///     返回 RESTful 风格结果集
    /// </summary>
    private static RestfulInfo<dynamic> RestfulResult(Enums.StatusCodes statusCode, object data = default
                                                    , object            message = default)
    {
        return new RestfulInfo<dynamic> { Code = statusCode, Data = data, Msg = message };
    }
}