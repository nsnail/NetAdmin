using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：核实验证码
/// </summary>
public abstract record VerifyCodeReq : Sys_VerifyCode
{
    /// <inheritdoc cref="Sys_VerifyCode.Code" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [VerifyCode]
    public override string Code { get; init; }

    /// <inheritdoc cref="Sys_VerifyCode.DestDevice" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.目标设备))]
    public override string DestDevice { get; init; }
}