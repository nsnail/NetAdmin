using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     响应：查询配置
/// </summary>
public sealed record QueryConfigRsp : Sys_Config
{
    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterConfirm" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool UserRegisterConfirm { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterDept" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new QueryDeptRsp UserRegisterDept { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterDeptId { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterRole" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public new QueryRoleRsp UserRegisterRole { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterRoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterRoleId { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}