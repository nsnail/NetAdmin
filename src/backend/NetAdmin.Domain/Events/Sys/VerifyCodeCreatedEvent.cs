using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     验证码创建事件
/// </summary>
public sealed record VerifyCodeCreatedEvent : DataAbstraction, IEventSourceGeneric<QueryVerifyCodeRsp>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="VerifyCodeCreatedEvent" /> class.
    /// </summary>
    public VerifyCodeCreatedEvent(QueryVerifyCodeRsp data)
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
    public QueryVerifyCodeRsp Data { get; }

    /// <summary>
    ///     事件 Id
    /// </summary>
    public string EventId => nameof(VerifyCodeCreatedEvent);

    /// <summary>
    ///     事件承载（携带）数据
    /// </summary>
    public object Payload { get; }
}