namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     用户名验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class UserNameAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserNameAttribute" /> class.
    /// </summary>
    public UserNameAttribute() //
        : base(Chars.RGX_USERNAME)
    {
        ErrorMessageResourceType = typeof(Ln);
    }

    /// <inheritdoc />
    public override bool IsValid(object value)
    {
        if (!base.IsValid(value)) {
            ErrorMessageResourceName = nameof(Ln.用户名长度4位以上);
            return false;
        }

        if (!new MobileAttribute().IsValid(value)) {
            return true;
        }

        // 不能是手机号
        ErrorMessageResourceName = nameof(Ln.用户名不能是手机号);
        return false;
    }
}