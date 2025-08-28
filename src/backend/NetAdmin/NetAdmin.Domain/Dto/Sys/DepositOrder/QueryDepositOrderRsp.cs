using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     响应：查询充值订单
/// </summary>
public record QueryDepositOrderRsp : Sys_DepositOrder
{
    /// <inheritdoc cref="Sys_DepositOrder.ActualPayAmount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ActualPayAmount { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.DepositOrderStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DepositOrderStatues DepositOrderStatus { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.DepositPoint" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DepositPoint { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldModifiedTime.ModifiedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? ModifiedTime { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.Owner" />
    public new virtual QueryUserRsp Owner { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerDeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerDeptId { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.PaidAccount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string PaidAccount { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.PaidTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? PaidTime { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.PaymentFinger" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string PaymentFinger { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.PaymentMode" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override PaymentModes PaymentMode { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.ReceiptAccount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ReceiptAccount { get; init; }

    /// <inheritdoc cref="Sys_DepositOrder.ToPointRate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int ToPointRate { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}