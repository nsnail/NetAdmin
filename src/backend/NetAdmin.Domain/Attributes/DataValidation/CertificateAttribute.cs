namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     证件号码
/// </summary>
public sealed class CertificateAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CertificateAttribute" /> class.
    /// </summary>
    public CertificateAttribute() //
        : base(Chars.RGX_CERTIFICATE)
    {
        ErrorMessageResourceName = nameof(Ln.有效证件号码);
        ErrorMessageResourceType = typeof(Ln);
    }
}