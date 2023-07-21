namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     邀请码验证器
/// </summary>
public sealed class InviteCodeAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InviteCodeAttribute" /> class.
    /// </summary>
    public InviteCodeAttribute() //
        : base(Chars.RGX_INVITE_CODE)
    {
        ErrorMessageResourceName = nameof(Ln.邀请码不正确);
        ErrorMessageResourceType = typeof(Ln);
    }
}