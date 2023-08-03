using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     请求日志事件
/// </summary>
public sealed record RequestLogEvent : DataAbstraction, IEventSourceGeneric<CreateRequestLogReq>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogEvent" /> class.
    /// </summary>
    public RequestLogEvent(CreateRequestLogReq data)
    {
        Data = data;
    }

    /// <summary>
    ///     取消任务 Token
    /// </summary>
    /// <remarks>
    ///     用于取消本次消息处理
    /// </remarks>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    ///     事件创建时间
    /// </summary>
    public DateTime CreatedTime { get; }

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public CreateRequestLogReq Data { get; }

    /// <summary>
    ///     事件 Id
    /// </summary>
    public string EventId => nameof(RequestLogEvent);

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public object Payload { get; }
}