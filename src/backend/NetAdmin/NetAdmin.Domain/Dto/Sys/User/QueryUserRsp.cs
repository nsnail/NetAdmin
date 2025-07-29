using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.UserInvite;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：查询用户
/// </summary>
public record QueryUserRsp : Sys_User
{
    /// <inheritdoc cref="Sys_User.Avatar" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Avatar { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? CreatedUserId { get; init; }

    /// <inheritdoc cref="IFieldCreatedUser.CreatedUserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CreatedUserName { get; init; }

    /// <inheritdoc cref="Sys_User.Dept" />
    public new virtual QueryDeptRsp Dept { get; init; }

    /// <summary>
    ///     本部门以及子部门编号
    /// </summary>
    public IReadOnlyCollection<long?> DeptIds { get; init; }

    /// <inheritdoc cref="Sys_User.Email" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Email { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_User.Invite" />
    public new virtual QueryUserInviteRsp Invite { get; init; }

    /// <inheritdoc cref="Sys_User.InviteCode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string InviteCode { get; init; }

    /// <inheritdoc cref="Sys_User.LastLoginTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? LastLoginTime { get; init; }

    /// <inheritdoc cref="Sys_User.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Mobile { get; init; }

    /// <inheritdoc cref="Sys_User.Roles" />
    public new virtual IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc cref="Sys_User.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; set; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string UserName { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}