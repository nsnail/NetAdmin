using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Domain.Events;

/// <summary>
///     短信创建事件
/// </summary>
public sealed record SmsCodeCreatedEvent : DataAbstraction, IEventSourceGeneric<QuerySmsRsp>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsCodeCreatedEvent" /> class.
    /// </summary>
    public SmsCodeCreatedEvent(QuerySmsRsp data)
    {
        Data = data;
    }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public QuerySmsRsp Data { get; }

    /// <inheritdoc />
    public string EventId => nameof(SmsCodeCreatedEvent);

    /// <inheritdoc />
    public object Payload { get; }
}