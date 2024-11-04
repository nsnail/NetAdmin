namespace NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：设置验证码状态
/// </summary>
public sealed record SetVerifyCodeStatusReq : CreateVerifyCodeReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}