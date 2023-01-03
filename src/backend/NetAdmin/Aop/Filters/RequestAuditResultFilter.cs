using Furion.DependencyInjection;
using Furion.EventBus;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAdmin.Aop.Filters;

/// <summary>
///     请求审计日志
/// </summary>
[SuppressSniffer]
public class RequestAuditResultFilter : IAsyncAlwaysRunResultFilter
{
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestAuditResultFilter" /> class.
    ///     构造函数
    /// </summary>
    public RequestAuditResultFilter(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    /// <inheritdoc />
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        RequestAuditActionFilter.AuditDatas.TryGetValue(Environment.CurrentManagedThreadId, out var auditData);
        if (auditData is null) {
            return;
        }

        var resultExecutedContext = await next();
        auditData.ResponseStatusCode = (ushort)resultExecutedContext.HttpContext.Response.StatusCode;
        RequestAuditActionFilter.AuditDatas.Remove(Environment.CurrentManagedThreadId, out _);

        // 发布审计事件
        await _eventPublisher.PublishAsync( //
            $"{nameof(RequestAuditResultFilter)}.{nameof(OnResultExecutionAsync)}", auditData);
    }
}