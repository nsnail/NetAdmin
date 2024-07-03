namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     响应：发送验证码
/// </summary>
public sealed record SendVerifyCodeRsp : Sys_VerifyCode
{
    #if DEBUG
    /// <inheritdoc cref="Sys_VerifyCode.Code" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Code { get; init; }
    #endif
}