namespace NetAdmin.DataContract.Attributes.DataValidation;

/// <summary>
///     用户名验证器
/// </summary>
public class UserNameAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserNameAttribute" /> class.
    /// </summary>
    public UserNameAttribute() //
        : base(Chars.RGX_USERNAME)
    {
        ErrorMessageResourceName = nameof(Ln.More_than_4_digits_alphanumeric_underline);
        ErrorMessageResourceType = typeof(Ln);
    }
}