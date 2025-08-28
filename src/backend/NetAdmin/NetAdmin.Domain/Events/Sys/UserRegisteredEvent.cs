using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     用户注册事件
/// </summary>
public sealed record UserRegisteredEvent : EventData<UserInfoRsp>
{
    /// <inheritdoc />
    public UserRegisteredEvent(UserInfoRsp payLoad)
        : base(payLoad) {
    }
}