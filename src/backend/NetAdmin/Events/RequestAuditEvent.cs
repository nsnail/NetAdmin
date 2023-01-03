using Furion.DependencyInjection;
using Furion.EventBus;
using Mapster;
using NetAdmin.Aop.Filters;
using NetAdmin.Api.Sys;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Extensions;
using NetAdmin.Repositories;
using NSExt.Extensions;

namespace NetAdmin.Events;

/// <summary>
///     请求审计事件
/// </summary>
public class RequestAuditEvent : IEventSubscriber, ISingleton, IDisposable
{
    private readonly IServiceScope _scope;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestAuditEvent" /> class.
    /// </summary>
    /// <param name="serviceProvider">serviceProvider</param>
    public RequestAuditEvent(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
    }

    /// <summary>
    ///     Finalizes an instance of the <see cref="RequestAuditEvent" /> class.
    /// </summary>
    ~RequestAuditEvent()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // Violates rule
    }

    /// <summary>
    ///     保存到数据库
    /// </summary>
    [EventSubscribe($"{nameof(RequestAuditResultFilter)}.{nameof(RequestAuditResultFilter.OnResultExecutionAsync)}")]
    public async Task SaveToDb(EventHandlerExecutingContext context)
    {
        if (context.Source.Payload is not TbSysOperationLog tbSysOperationLog) {
            return;
        }

        // 截断过长的ResponseResult
        const int cutThreshold = 1000;
        if (tbSysOperationLog.ResponseResult?.Length > cutThreshold) {
            tbSysOperationLog.ResponseResult = $"{tbSysOperationLog.ResponseResult.Sub(0, cutThreshold)}...";
        }

        await _scope.ServiceProvider.GetRequiredService<Repository<TbSysOperationLog>>().InsertAsync(tbSysOperationLog);

        //登录日志
        if (tbSysOperationLog.Action.Equals(nameof(IUserApi.Login), StringComparison.OrdinalIgnoreCase) &&
            tbSysOperationLog.Controller.Equals(nameof(UserApi).TrimEndApi(), StringComparison.OrdinalIgnoreCase)) {
            var tbSysLoginLog = tbSysOperationLog.Adapt<TbSysLoginLog>();
            await _scope.ServiceProvider.GetRequiredService<Repository<TbSysLoginLog>>().InsertAsync(tbSysLoginLog);
        }
    }

    /// <summary>
    ///     Dispose
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing) {
            _scope?.Dispose();
        }
    }
}