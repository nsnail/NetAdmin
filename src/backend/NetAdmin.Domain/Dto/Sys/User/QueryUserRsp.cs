using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Position;
using NetAdmin.Domain.Dto.Sys.Role;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：查询用户
/// </summary>
public record QueryUserRsp : TbSysUser
{
    /// <summary>
    ///     是否激活
    /// </summary>
    public bool Activated => BitSet.HasFlag(UserBits.Activated);

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <inheritdoc cref="TbSysUser.Avatar" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Avatar { get; init; }

    /// <inheritdoc cref="IFieldAdd.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <summary>
    ///     部门
    /// </summary>
    public new QueryDeptRsp Dept { get; init; }

    /// <inheritdoc cref="TbSysUser.Email" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Email { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="TbSysUser.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Mobile { get; init; }

    /// <summary>
    ///     岗位列表
    /// </summary>
    public new IEnumerable<QueryPositionRsp> Positions { get; init; }

    /// <summary>
    ///     角色列表
    /// </summary>
    public new IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string UserName { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}