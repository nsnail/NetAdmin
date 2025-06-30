namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     请求：创建配置
/// </summary>
public record CreateConfigReq : Sys_Config
{
    /// <inheritdoc cref="Sys_Config.CnyToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Range(1, int.MaxValue)]
    public override int CnyToPointRate { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="Sys_Config.RegisterInviteRequired" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool RegisterInviteRequired { get; init; }

    /// <inheritdoc cref="Sys_Config.RegisterMobileRequired" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool RegisterMobileRequired { get; init; }

    /// <inheritdoc cref="Sys_Config.Trc20ReceiptAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Length(34, 34)]
    public override string Trc20ReceiptAddress { get; init; }

    /// <inheritdoc cref="Sys_Config.UsdToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Range(1, int.MaxValue)]
    public override int UsdToPointRate { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterConfirm" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool UserRegisterConfirm { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterDeptId { get; init; }

    /// <inheritdoc cref="Sys_Config.UserRegisterRoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long UserRegisterRoleId { get; init; }
}