using System.Reflection;
using FreeSql;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using NSExt.Extensions;

namespace NetAdmin.Host.Aop;

/// <summary>
///     事务拦截器
/// </summary>
[SuppressSniffer]
public class TransactionInterceptor : IAsyncActionFilter
{
    private readonly ILogger<TransactionInterceptor> _logger;
    private readonly UnitOfWorkManager               _uowManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionInterceptor" /> class.
    ///     事务拦截器
    /// </summary>
    public TransactionInterceptor(ILogger<TransactionInterceptor> logger, UnitOfWorkManager uowManager)
    {
        _logger     = logger;
        _uowManager = uowManager;
    }

    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 跳过没有事务特性标记的方法
        if (context.HttpContext.GetControllerActionDescriptor()
                   .MethodInfo.GetCustomAttribute<TransactionAttribute>() is null) {
            await next();
            return;
        }

        using var unitOfWork = _uowManager.Begin();
        var       hashCode   = unitOfWork.GetHashCode();
        try {
            _logger.Info($"{Str.Transaction_starting}: {hashCode}");
            var result = await next();
            if (result.Exception is not null) {
                throw result.Exception;
            }

            unitOfWork.Commit();
            _logger.Info($"{Str.Transaction_commited}: {hashCode}");
        }
        catch (Exception) {
            unitOfWork.Rollback();
            _logger.Error($"{Str.Transaction_rollbacked}: {hashCode}");
            throw;
        }
    }
}