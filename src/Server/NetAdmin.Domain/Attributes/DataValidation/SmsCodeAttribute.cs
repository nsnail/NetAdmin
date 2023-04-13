namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     短信码验证器
/// </summary>
public sealed class SmsCodeAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsCodeAttribute" /> class.
    /// </summary>
    public SmsCodeAttribute() //
        : base(Chars.RGX_SMSCODE)
    {
        ErrorMessageResourceName = nameof(Ln.Incorrect_SMS_verification_code);
        ErrorMessageResourceType = typeof(Ln);
    }
}