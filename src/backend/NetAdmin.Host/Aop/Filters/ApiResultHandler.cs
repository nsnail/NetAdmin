using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAdmin.Host.Aop.Filters;

/// <summary>
///     Api结果格式化处理器
/// </summary>
/// <remarks>
///     约定：
///     1、业务异常需要设置HttpStatusCode与成功请求区分，不要统一返回200（ 为了方便前端调试：在浏览器F12中快速观察失败请求【红色高亮】）
///     2、不得占用常见HttpStatusCode，例如4xx、5xx，以免与传输层错误混淆，干扰运维。
///     实现：
///     1、本系统代码覆盖范围内占用4个HttpStatusCode：200（表示业务成功）、401（身份未确认）、403（权限不足）、900（其他所有业务异常）
///     2、当HttpStatusCode为900时，通过子码（JsonBody里面的Code区分具体异常），同时将子码写入RspHeader中，方便日志系统快速筛选归类。
///     3、子码定义，见枚举 <see cref="Enums.RspCodes" />
/// </remarks>
[SuppressSniffer]
[UnifyModel(typeof(RestfulInfo<>))]
public class ApiResultHandler : IUnifyResultProvider
{
    /// <summary>
    ///     发生异常
    /// </summary>
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        var errorCode = metadata.OriginErrorCode is Enums.RspCodes code ? code : Enums.RspCodes.Unexpected;
        var result    = RestfulResult(errorCode, metadata.Data, metadata.Errors);

        SetRspCodeToHeader(context.HttpContext, errorCode);

        return new JsonResult(result) { StatusCode = Numbers.HTTP_STATUS_FAIL };
    }

    /// <summary>
    ///     HTTP状态码处理
    /// </summary>
    public Task OnResponseStatusCodes( //
        HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
    {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     请求成功
    /// </summary>
    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        SetRspCodeToHeader(context.HttpContext, Enums.RspCodes.Succeed);
        return new JsonResult(RestfulResult(Enums.RspCodes.Succeed, data));
    }

    /// <summary>
    ///     校验失败
    /// </summary>
    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        SetRspCodeToHeader(context.HttpContext, Enums.RspCodes.InvalidInput);
        return new JsonResult(RestfulResult(Enums.RspCodes.InvalidInput, metadata.Data, metadata.ValidationResult)) {
                   StatusCode = Numbers.HTTP_STATUS_FAIL
               };
    }

    /// <summary>
    ///     返回 RESTful 风格结果集
    /// </summary>
    private static RestfulInfo<dynamic> RestfulResult(Enums.RspCodes rspCode, object data = default
                                                    , object         message = default)
    {
        return new RestfulInfo<dynamic> { Code = rspCode, Data = data, Msg = message };
    }

    private static void SetRspCodeToHeader(HttpContext context, Enums.RspCodes rspCode)
    {
        context.Response.Headers[nameof(Enums.RspCodes)] = Enum.GetName(rspCode);
    }
}