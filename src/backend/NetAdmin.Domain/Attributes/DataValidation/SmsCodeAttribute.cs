namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     短信码验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class SmsCodeAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsCodeAttribute" /> class.
    /// </summary>
    public SmsCodeAttribute() //
        : base(Chars.RGX_SMS_CODE)
    {
        ErrorMessageResourceName = nameof(Ln.短信验证码不正确);
        ErrorMessageResourceType = typeof(Ln);
    }
}