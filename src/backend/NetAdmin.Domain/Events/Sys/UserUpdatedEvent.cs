using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     用户被更新事件
/// </summary>
public sealed record UserUpdatedEvent : DataAbstraction, IEventSourceGeneric<QueryUserRsp>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserUpdatedEvent" /> class.
    /// </summary>
    public UserUpdatedEvent(QueryUserRsp data)
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
    public QueryUserRsp Data { get; }

    /// <summary>
    ///     事件 Id
    /// </summary>
    public string EventId => nameof(UserUpdatedEvent);

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public object Payload { get; }
}