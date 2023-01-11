using System.ComponentModel.DataAnnotations;

namespace NetAdmin.Aop.Attributes.DataValidation;

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
        // 解决RegularExpressionAttribute 认为string.Empty是有效值的问题。
        return !base.IsValid(value) || value is not string s || !string.IsNullOrEmpty(s);
    }
}