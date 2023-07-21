namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     用户名验证器
/// </summary>
public sealed class UserNameAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserNameAttribute" /> class.
    /// </summary>
    public UserNameAttribute() //
        : base(Chars.RGX_USERNAME)
    {
        ErrorMessageResourceName = nameof(Ln._4位以上);
        ErrorMessageResourceType = typeof(Ln);
    }

    /// <inheritdoc />
    public override bool IsValid(object value)
    {
        var ret = base.IsValid(value);
        if (!ret) {
            return false;
        }

        ret = new MobileAttribute().IsValid(value);
        if (!ret) {
            return true;
        }

        // 不能是手机号
        ErrorMessageResourceName = nameof(Ln.用户名不能是手机号);
        return false;
    }
}