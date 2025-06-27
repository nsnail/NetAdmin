namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     响应：查询充值订单
/// </summary>
public sealed record GetDepositConfigRsp : Sys_Config
{
    /// <inheritdoc cref="Sys_Config.CnyToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int CnyToPointRate { get; init; }

    /// <inheritdoc cref="Sys_Config.Trc20ReceiptAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Trc20ReceiptAddress { get; init; }

    /// <inheritdoc cref="Sys_Config.UsdToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int UsdToPointRate { get; init; }
}