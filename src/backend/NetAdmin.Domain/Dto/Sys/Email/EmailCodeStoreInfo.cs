namespace NetAdmin.Domain.Dto.Sys.Email;

/// <summary>
///     信息：邮件存储
/// </summary>
public record EmailCodeStoreInfo : EmailCodeInfo
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Code { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmailAddress { get; init; }
}