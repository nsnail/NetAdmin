namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：修改粉丝角色
/// </summary>
public record SetFansRoleReq : Sys_UserInvite
{
    /// <inheritdoc />
    public override long Id { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    public long RoleId { get; init; }
}