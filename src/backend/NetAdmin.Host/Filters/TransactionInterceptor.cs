using NetAdmin.Host.Attributes;
using NetAdmin.Host.Extensions;

namespace NetAdmin.Host.Filters;

/// <summary>
///     事务拦截器
/// </summary>
[SuppressSniffer]
public sealed class TransactionInterceptor : IAsyncActionFilter
{
    private readonly UnitOfWorkManager _uowManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionInterceptor" /> class.
    /// </summary>
    public TransactionInterceptor(UnitOfWorkManager uowManager)
    {
        _uowManager = uowManager;
    }

    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 跳过没有事务特性标记的方法
        if (context.HttpContext.GetControllerActionDescriptor().MethodInfo.GetCustomAttribute<TransactionAttribute>() ==
            null) {
            _ = await next();
            return;
        }

        // 事务操作
        await _uowManager.AtomicOperateAsync(async () => {
            var result = await next();
            if (result.Exception != null) {
                throw result.Exception;
            }
        });
    }
}