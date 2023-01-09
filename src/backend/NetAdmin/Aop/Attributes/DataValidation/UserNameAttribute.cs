using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;

namespace NetAdmin.Aop.Attributes.DataValidation;

/// <summary>
///     用户名验证器
/// </summary>
public class UserNameAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserNameAttribute" /> class.
    /// </summary>
    public UserNameAttribute() //
        : base(Strings.RGX_USERNAME)
    {
        ErrorMessageResourceName = nameof(Str.MORE_THAN_4_DIGITS_ALPHANUMERIC_UNDERLINE);
        ErrorMessageResourceType = typeof(Str);
    }
}