using System.ComponentModel.DataAnnotations;

namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     电话验证器（手机或固话）
/// </summary>
public class PhoneAttribute : ValidationAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PhoneAttribute" /> class.
    /// </summary>
    public PhoneAttribute()
    {
        ErrorMessageResourceName = nameof(Ln.Mobile_phone_number_or_landline_number);
        ErrorMessageResourceType = typeof(Ln);
    }

    /// <inheritdoc />
    public override bool IsValid(object value)
    {
        return new MobileAttribute().IsValid(value) || new TelephoneAttribute().IsValid(value);
    }
}