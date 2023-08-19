using NetAdmin.Domain.Attributes.DataValidation;

namespace NetAdmin.Domain.Dto.Sys.Email;

/// <summary>
///     请求：核实邮箱验证码
/// </summary>
public record VerifyEmailCodeReq : EmailCodeInfo
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.验证码))]
    public override string Code { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.电子邮箱))]
    public override string EmailAddress { get; init; }
}