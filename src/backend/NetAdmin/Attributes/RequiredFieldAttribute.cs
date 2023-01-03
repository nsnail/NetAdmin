using System.ComponentModel.DataAnnotations;

namespace NetAdmin.Attributes;

/// <summary>
///     必填项约束
/// </summary>
public class RequiredFieldAttribute : RequiredAttribute
{
    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return !string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage : $"{name} 是必填项";
    }
}