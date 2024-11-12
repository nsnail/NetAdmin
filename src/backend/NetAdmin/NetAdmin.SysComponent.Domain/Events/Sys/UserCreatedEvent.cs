using NetAdmin.Domain.Events;
using NetAdmin.SysComponent.Domain.Dto.Sys.User;

namespace NetAdmin.SysComponent.Domain.Events.Sys;

/// <summary>
///     用户被创建事件
/// </summary>
public sealed record UserCreatedEvent : DataAbstraction, IEventSourceGeneric<UserInfoRsp>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserCreatedEvent" /> class.
    /// </summary>
    public UserCreatedEvent(UserInfoRsp       data, DateTime createdTime = default, bool isConsumOnce = false, object payload = default
                          , CancellationToken cancellationToken = default)
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
    public string EventId => nameof(UserCreatedEvent);

    /// <inheritdoc />
    public bool IsConsumOnce { get; }

    /// <inheritdoc />
    public object Payload { get; }
}