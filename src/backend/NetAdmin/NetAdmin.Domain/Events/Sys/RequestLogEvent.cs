using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     请求日志事件
/// </summary>
public sealed record RequestLogEvent : EventData<CreateRequestLogReq>
{
    /// <inheritdoc />
    public RequestLogEvent(CreateRequestLogReq payLoad) //
        : base(payLoad) { }
}