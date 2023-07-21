namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     非空验证器
/// </summary>
public sealed class CultureRequiredAttribute : RequiredAttribute
{
    /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
    /// <param name="name">The name to include in the formatted message.</param>
    /// <exception cref="T:System.InvalidOperationException">The current attribute is malformed.</exception>
    /// <returns>An instance of the formatted error message.</returns>
    public override string FormatErrorMessage(string name)
    {
        return $"{ErrorMessageString.NullOrEmpty(name)} {Ln.不能为空}";
    }
}