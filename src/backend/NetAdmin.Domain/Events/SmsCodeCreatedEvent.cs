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
    public QuerySmsRsp Data { get; }

    /// <summary>
    ///     事件 Id
    /// </summary>
    public string EventId => nameof(SmsCodeCreatedEvent);

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public object Payload { get; }
}