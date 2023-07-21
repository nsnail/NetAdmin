namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     手机号验证器
/// </summary>
public sealed class MobileAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MobileAttribute" /> class.
    /// </summary>
    public MobileAttribute() //
        : base(Chars.RGX_MOBILE)
    {
        ErrorMessageResourceName = nameof(Ln.可用的手机号);
        ErrorMessageResourceType = typeof(Ln);
    }
}