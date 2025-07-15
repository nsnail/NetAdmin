namespace NetAdmin.Domain.Dto.Sys.WalletFrozen;

/// <summary>
///     请求：将状态设置为解冻
/// </summary>
public record SetStatusToThawedReq : Sys_WalletFrozen
{
    /// <inheritdoc cref="Sys_WalletFrozen.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}