namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     JSON文本验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class JsonStringAttribute : ValidationAttribute
{
    /// <inheritdoc />
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return (value as string).IsJsonString()
            ? ValidationResult.Success
            : new ValidationResult(Ln.非JSON字符串, new[] { validationContext.MemberName });
    }
}