namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     电话验证器（手机或固话）
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CulturePhoneAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CulturePhoneAttribute" /> class.
    /// </summary>
    public CulturePhoneAttribute()
    {
        ErrorMessageResourceName = nameof(Ln.可用的手机号或座机号);
        ErrorMessageResourceType = typeof(Ln);
    }

    /// <inheritdoc />
    public override bool IsValid(object value)
    {
        return new MobileAttribute().IsValid(value) || new TelephoneAttribute().IsValid(value);
    }
}