using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;

namespace NetAdmin.Aop.Attributes.DataValidation;

/// <summary>
///     手机号验证器
/// </summary>
public class MobileAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MobileAttribute" /> class.
    /// </summary>
    public MobileAttribute() //
        : base(Strings.RGX_MOBILE)
    {
        ErrorMessageResourceName = nameof(Str.Mobile_phone_number_that_can_be_used_normally);
        ErrorMessageResourceType = typeof(Str);
    }
}