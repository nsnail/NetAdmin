namespace NetAdmin.DataContract.Attributes.DataValidation;

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
        ErrorMessageResourceName = nameof(Str.Number_letter_combination_of_more_than_8_digits);
        ErrorMessageResourceType = typeof(Str);
    }
}