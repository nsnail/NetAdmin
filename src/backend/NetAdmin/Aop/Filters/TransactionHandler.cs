using FreeSql;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAdmin.Aop.Filters;

/// <summary>
///     事务拦截器
/// </summary>
[SuppressSniffer]
public class TransactionHandler : IAsyncActionFilter
{
    private readonly ILogger<TransactionHandler> _logger;
    private readonly UnitOfWorkManager           _uowManager;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TransactionHandler" /> class.
    ///     事务拦截器
    /// </summary>
    public TransactionHandler(ILogger<TransactionHandler> logger, UnitOfWorkManager uowManager)
    {
        _logger     = logger;
        _uowManager = uowManager;
    }

    /// <inheritdoc />
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        using var unitOfWork = _uowManager.Begin();
        var       hashCode   = unitOfWork.GetHashCode();
        try {
            _logger.LogInformation("事务 {HashCode} 开始", hashCode);
            var result = await next();
            if (result.Exception is not null) {
                throw result.Exception;
            }

            unitOfWork.Commit();
            _logger.LogInformation("事务 {HashCode} 完成", hashCode);
        }
        catch (Exception) {
            unitOfWork.Rollback();
            _logger.LogError("事务 {HashCode} 回滚", hashCode);
            throw;
        }
    }
}