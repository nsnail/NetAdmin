using Furion.DependencyInjection;
using Furion.EventBus;
using Mapster;
using NetAdmin.Api.Aop;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Service.Sys;
using NetAdmin.Application.Service.Sys.Implements;
using NetAdmin.DataContract.DbMaps;
using NSExt.Extensions;

namespace NetAdmin.Api.Events.Subscribers;

/// <summary>
///     请求日志记录
/// </summary>
public class RequestLogger : IEventSubscriber, ISingleton, IDisposable
{
    private static readonly string        _userApiControllerName = nameof(UserService)[..^3];
    private readonly        IServiceScope _scope;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogger" /> class.
    /// </summary>
    /// <param name="serviceProvider">serviceProvider</param>
    public RequestLogger(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
    }

    /// <summary>
    ///     Finalizes an instance of the <see cref="RequestLogger" /> class.
    /// </summary>
    ~RequestLogger()
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
    [EventSubscribe($"{nameof(RequestAuditFilter)}.{nameof(RequestAuditFilter.OnActionExecutionAsync)}")]
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

        // 登录日志
        if (tbSysOperationLog.Action.Equals(nameof(IUserService.Login), StringComparison.OrdinalIgnoreCase) &&
            tbSysOperationLog.Controller.Equals(_userApiControllerName, StringComparison.OrdinalIgnoreCase)) {
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