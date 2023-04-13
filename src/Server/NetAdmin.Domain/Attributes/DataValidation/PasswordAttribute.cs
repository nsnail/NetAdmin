namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     密码验证器
/// </summary>
public sealed class PasswordAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PasswordAttribute" /> class.
    /// </summary>
    public PasswordAttribute() //
        : base(Chars.RGX_PASSWORD)
    {
        ErrorMessageResourceName = nameof(Ln.Number_letter_combination_of_more_than_8_digits);
        ErrorMessageResourceType = typeof(Ln);
    }
}