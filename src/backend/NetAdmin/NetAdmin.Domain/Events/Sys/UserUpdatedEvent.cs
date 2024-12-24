using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     用户被更新事件
/// </summary>
public sealed record UserUpdatedEvent : DataAbstraction, IEventSourceGeneric<UserInfoRsp>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserUpdatedEvent" /> class.
    /// </summary>
    public UserUpdatedEvent(UserInfoRsp       data, DateTime createdTime = default, bool isConsumeOnce = false, object payload = null
                          , CancellationToken cancellationToken = default)
    {
        Data              = data;
        CancellationToken = cancellationToken;
        CreatedTime       = createdTime;
        IsConsumeOnce     = isConsumeOnce;
        Payload           = payload;
    }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public UserInfoRsp Data { get; }

    /// <inheritdoc />
    public string EventId => nameof(UserUpdatedEvent);

    /// <inheritdoc />
    public bool IsConsumeOnce { get; }

    /// <inheritdoc />
    public object Payload { get; }
}