using System.ComponentModel.DataAnnotations;

namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     正则表达式验证器
/// </summary>
public class RegexAttribute : RegularExpressionAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RegexAttribute" /> class.
    /// </summary>
    protected RegexAttribute(string pattern) //
        : base(pattern) { }

    /// <inheritdoc />
    public override bool IsValid(object value)
    {
        var ret = base.IsValid(value);
        return ret;
    }
}