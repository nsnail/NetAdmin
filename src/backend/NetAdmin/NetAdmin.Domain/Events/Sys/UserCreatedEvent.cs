using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     用户被创建事件
/// </summary>
public sealed record UserCreatedEvent : EventData<UserInfoRsp>
{
    /// <inheritdoc />
    public UserCreatedEvent(UserInfoRsp payLoad)
        : base(payLoad) {
    }
}