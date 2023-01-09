using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;

namespace NetAdmin.Aop.Attributes.DataValidation;

/// <summary>
///     密码验证器
/// </summary>
public class PasswordAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PasswordAttribute" /> class.
    /// </summary>
    public PasswordAttribute() //
        : base(Strings.RGX_PASSWORD)
    {
        ErrorMessageResourceName = nameof(Str.NUMBER_LETTER_COMBINATION_OF_MORE_THAN_8_DIGITS);
        ErrorMessageResourceType = typeof(Str);
    }
}