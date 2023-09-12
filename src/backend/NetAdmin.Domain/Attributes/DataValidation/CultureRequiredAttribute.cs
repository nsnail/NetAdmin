namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     非空验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CultureRequiredAttribute : RequiredAttribute
{
    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return $"{ErrorMessageString.NullOrEmpty(name)} {Ln.不能为空}";
    }
}