namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     证件号码
/// </summary>
public class CertificateAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CertificateAttribute" /> class.
    /// </summary>
    public CertificateAttribute() //
        : base(Chars.RGX_CERTIFICATE)
    {
        ErrorMessageResourceName = nameof(Ln.Please_enter_a_valid_certificate_number);
        ErrorMessageResourceType = typeof(Ln);
    }
}