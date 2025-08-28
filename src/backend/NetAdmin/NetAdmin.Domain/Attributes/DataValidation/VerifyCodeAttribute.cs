namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     验证码验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class VerifyCodeAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="VerifyCodeAttribute" /> class.
    /// </summary>
    public VerifyCodeAttribute()
        : base(Chars.RGX_VERIFY_CODE) {
        ErrorMessageResourceName = nameof(Ln.验证码不正确);
        ErrorMessageResourceType = typeof(Ln);
    }
}