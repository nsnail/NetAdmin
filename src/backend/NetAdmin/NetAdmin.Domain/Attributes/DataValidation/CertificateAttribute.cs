namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     证件号码验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CertificateAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CertificateAttribute" /> class.
    /// </summary>
    public CertificateAttribute()
        : base(Chars.RGX_CERTIFICATE) {
        ErrorMessageResourceName = nameof(Ln.无效证件号码);
        ErrorMessageResourceType = typeof(Ln);
    }
}