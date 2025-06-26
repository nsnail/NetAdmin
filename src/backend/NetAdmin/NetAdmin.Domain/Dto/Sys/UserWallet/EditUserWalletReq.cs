namespace NetAdmin.Domain.Dto.Sys.UserWallet;

/// <summary>
///     请求：编辑用户钱包
/// </summary>
public record EditUserWalletReq : CreateUserWalletReq
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}