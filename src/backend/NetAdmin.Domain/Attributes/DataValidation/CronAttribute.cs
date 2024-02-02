namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     时间表达式验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CronAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CronAttribute" /> class.
    /// </summary>
    public CronAttribute() //
        : base(Chars.RGX_CRON)
    {
        ErrorMessageResourceName = nameof(Ln.时间表达式);
        ErrorMessageResourceType = typeof(Ln);
    }
}