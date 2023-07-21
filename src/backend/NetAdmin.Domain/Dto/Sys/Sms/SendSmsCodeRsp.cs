using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Sms;

/// <summary>
///     响应：发送短信验证码
/// </summary>
public sealed record SendSmsCodeRsp : Sys_Sms
{
    #if DEBUG
    /// <inheritdoc cref="Sys_Sms.Code" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Code { get; init; }
    #endif
}