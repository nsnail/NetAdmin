using NetAdmin.Domain.Dto;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     ResourceExecutingContextExtensions
/// </summary>
public static class ResourceExecutingContextExtensions
{
    /// <summary>
    ///     设置失败结果
    /// </summary>
    public static void SetFailResult<T>(this ResourceExecutingContext me, ErrorCodes errorCode, string errorMsg = null)
        where T : RestfulInfo<object>, new()
    {
        me.Result                          = new JsonResult(new T { Code = errorCode, Msg = errorMsg });
        me.HttpContext.Response.StatusCode = Numbers.HTTP_STATUS_BIZ_FAIL;
    }
}