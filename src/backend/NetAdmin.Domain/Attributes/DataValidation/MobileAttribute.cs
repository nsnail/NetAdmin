namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     手机号验证器
/// </summary>
public class MobileAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MobileAttribute" /> class.
    /// </summary>
    public MobileAttribute() //
        : base(Chars.RGX_MOBILE)
    {
        ErrorMessageResourceName = nameof(Ln.Mobile_phone_number_that_can_be_used_normally);
        ErrorMessageResourceType = typeof(Ln);
    }
}