using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Position;
using NetAdmin.Domain.Dto.Sys.Role;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     响应：查询配置
/// </summary>
public record QueryConfigRsp : TbSysConfig
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterConfirm" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool UserRegisterConfirm { get; set; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterDept" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new QueryDeptRsp UserRegisterDept { get; init; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterDeptId { get; init; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterPos" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new QueryPositionRsp UserRegisterPos { get; init; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterPosId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterPosId { get; set; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterRole" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new QueryRoleRsp UserRegisterRole { get; init; }

    /// <inheritdoc cref="TbSysConfig.UserRegisterRoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterRoleId { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}