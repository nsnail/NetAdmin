using NetAdmin.Application.Extensions;
using NetAdmin.Host.Attributes;

namespace NetAdmin.Host.Filters;

/// <summary>
///     事务拦截器
/// </summary>
[SuppressSniffer]
public sealed class TransactionInterceptor(UnitOfWorkManager uowManager) : IAsyncActionFilter
{
    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 跳过没有事务特性标记的方法
        if (context.HttpContext.GetControllerActionDescriptor().MethodInfo.GetCustomAttribute<TransactionAttribute>() == null) {
            _ = await next().ConfigureAwait(false);
            return;
        }

        // 事务操作
        await uowManager.AtomicOperateAsync(async () => {
                            var result = await next().ConfigureAwait(false);
                            if (result.Exception != null) {
                                throw result.Exception;
                            }
                        })
                        .ConfigureAwait(false);
    }
}