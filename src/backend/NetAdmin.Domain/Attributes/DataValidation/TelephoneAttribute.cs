namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     固定电话验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class TelephoneAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TelephoneAttribute" /> class.
    /// </summary>
    public TelephoneAttribute() //
        : base(Chars.RGX_TELEPHONE)
    {
        ErrorMessageResourceName = nameof(Ln.分机号);
        ErrorMessageResourceType = typeof(Ln);
    }
}