namespace NetAdmin.Domain.Dto.Sys.WalletTrade;

/// <summary>
///     请求：转账
/// </summary>
public record TransferReq : Sys_WalletTrade
{
    /// <inheritdoc cref="Sys_WalletTrade.Amount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Amount { get; set; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    [UserId]
    [Range(1, long.MaxValue)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; set; }

    /// <inheritdoc />
    protected override IEnumerable<ValidationResult> ValidateInternal(ValidationContext validationContext)
    {
        yield return Amount <= 0 ? new ValidationResult(Ln.交易金额不正确, [nameof(Amount)]) : ValidationResult.Success;
    }
}