namespace NetAdmin.Domain.Dto.Sys.WalletTrade;

/// <summary>
///     请求：创建钱包交易
/// </summary>
public record CreateWalletTradeReq : Sys_WalletTrade, IValidatableObject
{
    private readonly TradeTypes _tradeType;

    /// <inheritdoc cref="Sys_WalletTrade.Amount" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Amount { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.BusinessOrderNumber" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override long? BusinessOrderNumber { get; init; }

    /// <inheritdoc cref="IFieldOwner.OwnerId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    [UserId]
    [Range(1, long.MaxValue)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.TradeDirection" />
    public override TradeDirections TradeDirection { get; init; }

    /// <inheritdoc cref="Sys_WalletTrade.TradeType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(TradeTypes))]
    public override TradeTypes TradeType {
        get => _tradeType;
        init {
            _tradeType     = value;
            TradeDirection = value.Attr<TradeAttribute>().Direction;
        }
    }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var tradeDirection = TradeType.Attr<TradeAttribute>().Direction;
        if (Amount == 0 || (tradeDirection == TradeDirections.Income && Amount < 0) || (tradeDirection == TradeDirections.Expense && Amount > 0)) {
            yield return new ValidationResult(Ln.交易金额不正确, [nameof(Amount)]);
        }

        yield return ValidationResult.Success;
    }
}