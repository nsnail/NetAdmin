namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     交易密码验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class PayPasswordAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PayPasswordAttribute" /> class.
    /// </summary>
    public PayPasswordAttribute()
        : base(Chars.RGX_PAY_PASSWORD) {
        ErrorMessageResourceName = nameof(Ln._6位数字);
        ErrorMessageResourceType = typeof(Ln);
    }
}