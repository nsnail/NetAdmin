namespace NetAdmin.Domain.Dto.Sys.Email;

/// <summary>
///     响应：发送邮箱验证码
/// </summary>
public sealed record SendEmailCodeRsp : EmailCodeInfo
{
    #if DEBUG

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Code { get; init; }

    #endif
}