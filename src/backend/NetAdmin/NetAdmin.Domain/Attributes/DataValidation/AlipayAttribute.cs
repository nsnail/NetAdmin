namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     支付宝验证器（手机或邮箱）
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class AlipayAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AlipayAttribute" /> class.
    /// </summary>
    public AlipayAttribute() {
        ErrorMessageResourceName = nameof(Ln.支付宝账号);
        ErrorMessageResourceType = typeof(Ln);
    }

    /// <inheritdoc />
    public override bool IsValid(object value) {
        return new MobileAttribute().IsValid(value) || new EmailAddressAttribute().IsValid(value);
    }
}