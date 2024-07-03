namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：查询验证码
/// </summary>
public sealed record QueryVerifyCodeReq : Sys_VerifyCode
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}