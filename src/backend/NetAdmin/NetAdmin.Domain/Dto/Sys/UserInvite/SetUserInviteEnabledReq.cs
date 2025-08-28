using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：设置用户启用状态
/// </summary>
public record SetUserInviteEnabledReq : EditUserInviteReq
{
    /// <inheritdoc cref="Sys_UserInvite.User" />
    public new virtual QueryUserRsp User { get; init; }
}