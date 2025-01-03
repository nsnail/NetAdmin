using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     用户被更新事件
/// </summary>
public sealed record UserUpdatedEvent : EventData<UserInfoRsp>
{
    /// <inheritdoc />
    public UserUpdatedEvent(UserInfoRsp payLoad) //
        : base(payLoad) { }
}