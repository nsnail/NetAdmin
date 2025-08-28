using Gurion.FriendlyException;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Dto;

namespace NetAdmin.Host.Filters;

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
///     3、子码定义，见枚举 <see cref="ErrorCodes" />
/// </remarks>
public abstract class ApiResultHandler<T>
    where T : RestfulInfo<object>, new()
{
    /// <summary>
    ///     发生异常
    /// </summary>
    public IActionResult OnException(
        ExceptionContext context
        , ExceptionMetadata metadata
    ) {
        var naException = context.Exception switch
        {
            NetAdminException ex => ex
            , _ => context.Exception.Message.Contains(Chars.FLG_DB_EXCEPTION_PRIMARY_KEY_CONFLICT)
                   || context.Exception.Message.Contains(Chars.FLG_DB_EXCEPTION_UNIQUE_CONSTRAINT_CONFLICT)
                   || context.Exception.Message.Contains(Chars.FLG_DB_EXCEPTION_IDX)
                ? new NetAdminInvalidOperationException(Ln.记录已存在)
                : null
        };
        var errorCode = naException?.Code ?? ErrorCodes.InternalError;

        // 超管显示异常明细
        var errorMsg = naException?.Message
                       ?? (App.GetService<ContextUserInfo>()?.Roles.Any(x => x.IgnorePermissionControl) == true
                           ? context.Exception.ToString()
                           : errorCode.ResDesc<ErrorCodes>());

        var result = RestfulResult(
            errorCode, metadata.Data, naException is NetAdminValidateException vEx ? vEx.ValidateResults : errorMsg, context.HttpContext.GetTraceId()
        );

        SetErrorCodeToHeader(context.HttpContext, errorCode);

        return new JsonResult(result) { StatusCode = Numbers.HTTP_STATUS_BIZ_FAIL };
    }

    /// <summary>
    ///     HTTP状态码处理
    /// </summary>
    public Task OnResponseStatusCodesAsync(
        HttpContext context
        , int statusCode
        , UnifyResultSettingsOptions unifyResultSettings = null
    ) {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     请求成功
    /// </summary>
    public IActionResult OnSucceeded(
        ActionExecutedContext _
        , object data
    ) {
        return new JsonResult(RestfulResult(0, data));
    }

    /// <summary>
    ///     校验失败
    /// </summary>
    public IActionResult OnValidateFailed(
        ActionExecutingContext context
        , ValidationMetadata metadata
    ) {
        SetErrorCodeToHeader(context.HttpContext, ErrorCodes.InvalidInput);

        return new JsonResult(RestfulResult(ErrorCodes.InvalidInput, metadata.Data, GetValidationResult(metadata.ValidationResult)))
        {
            StatusCode = Numbers.HTTP_STATUS_BIZ_FAIL
        };
    }

    private static object GetValidationResult(object validationResult) {
        var startWithDollar = false;
        try {
            return validationResult is Dictionary<string, string[]> dic
                ? dic.ToDictionary(
                    x => (startWithDollar = x.Key.StartsWith('$'))
                        ? x.Key[1..].TrimStart('.').NullOrEmpty(null) ?? throw new NetAdminInvalidInputException()
                        : x.Key, x => startWithDollar ? [Ln.参数格式不正确] : x.Value
                )
                : validationResult;
        }
        catch (NetAdminInvalidInputException) {
            return Ln.参数格式不正确;
        }
    }

    /// <summary>
    ///     返回 RESTful 风格结果集
    /// </summary>
    private static T RestfulResult(
        ErrorCodes errorCode
        , object data = null
        , object message = null
        , Guid? traceId = null
    ) {
        return new T { Code = errorCode, Data = data, Msg = message, TraceId = traceId };
    }

    /// <summary>
    ///     写入错误码到HttpHeader
    /// </summary>
    private static void SetErrorCodeToHeader(
        HttpContext context
        , ErrorCodes errorCode
    ) {
        context.Response.Headers[nameof(ErrorCodes)] = Enum.GetName(errorCode);
    }
}