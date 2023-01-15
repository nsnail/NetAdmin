namespace NetAdmin.Host.Events.Sources;

/// <summary>
///     用户登录事件
/// </summary>
public class UserLoginEvent : IEventSourceGeneric<CreateLoginLogReq>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserLoginEvent" /> class.
    /// </summary>
    public UserLoginEvent(CreateLoginLogReq data)
    {
        Data = data;
    }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public CreateLoginLogReq Data { get; }

    /// <inheritdoc />
    public string EventId => nameof(UserLoginEvent);

    /// <inheritdoc />
    public object Payload { get; }
}