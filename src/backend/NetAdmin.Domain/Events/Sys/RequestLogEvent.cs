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
    public RequestLogEvent(CreateRequestLogReq data, bool isConsumOnce = false, object payload = default
                         , DateTime            createdTime = default, CancellationToken cancellationToken = default)
    {
        Data              = data;
        IsConsumOnce      = isConsumOnce;
        Payload           = payload;
        CreatedTime       = createdTime;
        CancellationToken = cancellationToken;
    }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public CreateRequestLogReq Data { get; }

    /// <inheritdoc />
    public string EventId => nameof(RequestLogEvent);

    /// <inheritdoc />
    public bool IsConsumOnce { get; }

    /// <inheritdoc />
    public object Payload { get; }
}