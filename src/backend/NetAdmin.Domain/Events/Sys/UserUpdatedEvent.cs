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
    public UserUpdatedEvent(UserInfoRsp data,              DateTime createdTime = default, bool isConsumOnce = false
                          , object      payload = default, CancellationToken cancellationToken = default)
    {
        Data              = data;
        CancellationToken = cancellationToken;
        CreatedTime       = createdTime;
        IsConsumOnce      = isConsumOnce;
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
    public bool IsConsumOnce { get; }

    /// <inheritdoc />
    public object Payload { get; }
}