using NetAdmin.Host.Attributes;
using NetAdmin.Host.Extensions;

namespace NetAdmin.Host.Middlewares;

/// <summary>
///     删除 response json body 中value 为null的节点
/// </summary>
public sealed class RemoveNullNodeMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     主函数
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.GetMetadata<RemoveNullNodeAttribute>() is null) {
            return;
        }

        await context.RemoveJsonNodeWithNullValueAsync();
    }
}