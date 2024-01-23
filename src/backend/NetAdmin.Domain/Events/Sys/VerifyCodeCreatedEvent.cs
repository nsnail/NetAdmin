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

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public QueryVerifyCodeRsp Data { get; }

    /// <inheritdoc />
    public string EventId => nameof(VerifyCodeCreatedEvent);

    /// <inheritdoc />
    public bool IsConsumOnce { get; }

    /// <inheritdoc />
    public object Payload { get; }
}