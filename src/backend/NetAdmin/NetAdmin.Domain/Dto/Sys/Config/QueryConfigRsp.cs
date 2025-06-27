using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     响应：查询配置
/// </summary>
public record QueryConfigRsp : Sys_Config
{
    /// <inheritdoc cref="Sys_Config.CnyToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int CnyToPointRate { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Config.Trc20ReceiptAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Trc20ReceiptAddress { get; init; }

    /// <inheritdoc cref="Sys_Config.UsdToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int UsdToPointRate { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterConfirm" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool UserRegisterConfirm { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterDept" />
    public new virtual QueryDeptRsp UserRegisterDept { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterDeptId { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterRole" />
    public new virtual QueryRoleRsp UserRegisterRole { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterRoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterRoleId { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}