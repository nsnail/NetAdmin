using NetAdmin.Domain.Dto.Sys.Email;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     邮件创建事件
/// </summary>
public sealed record EmailCodeCreatedEvent : DataAbstraction, IEventSourceGeneric<EmailCodeStoreInfo>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailCodeCreatedEvent" /> class.
    /// </summary>
    public EmailCodeCreatedEvent(EmailCodeStoreInfo data)
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
    public EmailCodeStoreInfo Data { get; }

    /// <summary>
    ///     事件 Id
    /// </summary>
    public string EventId => nameof(EmailCodeCreatedEvent);

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public object Payload { get; }
}