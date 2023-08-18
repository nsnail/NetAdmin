namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     手机号验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class MobileAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MobileAttribute" /> class.
    /// </summary>
    public MobileAttribute() //
        : base(Chars.RGX_MOBILE)
    {
        ErrorMessageResourceName = nameof(Ln.手机号码);
        ErrorMessageResourceType = typeof(Ln);
    }
}