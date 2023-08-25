namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     中文姓名
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class ChineseNameAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ChineseNameAttribute" /> class.
    /// </summary>
    public ChineseNameAttribute() //
        : base(Chars.RGX_CHINESE_NAME)
    {
        ErrorMessageResourceName = nameof(Ln.中文姓名);
        ErrorMessageResourceType = typeof(Ln);
    }
}