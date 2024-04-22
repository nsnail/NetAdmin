namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     邮箱验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class EmailAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailAttribute" /> class.
    /// </summary>
    public EmailAttribute() //
        : base(Chars.RGXL_EMAIL)
    {
        ErrorMessageResourceName = nameof(Ln.电子邮箱);
        ErrorMessageResourceType = typeof(Ln);
    }
}