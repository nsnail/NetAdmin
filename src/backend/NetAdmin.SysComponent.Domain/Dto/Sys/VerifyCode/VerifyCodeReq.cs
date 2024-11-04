namespace NetAdmin.SysComponent.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：核实验证码
/// </summary>
public abstract record VerifyCodeReq : Sys_VerifyCode
{
    /// <inheritdoc cref="Sys_VerifyCode.Code" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码不能为空))]
    [VerifyCode]
    public override string Code { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.DestDevice" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.目标设备不能为空))]
    public override string DestDevice { get; init; }
}