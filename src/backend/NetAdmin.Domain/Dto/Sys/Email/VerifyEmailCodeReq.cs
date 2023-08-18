namespace NetAdmin.Domain.Dto.Sys.Email;

/// <summary>
///     请求：核实邮箱验证码
/// </summary>
public record VerifyEmailCodeReq : EmailCodeInfo
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Code { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmailAddress { get; init; }
}