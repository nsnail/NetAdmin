namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     正则表达式验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
#pragma warning disable DesignedForInheritance
public class RegexAttribute : RegularExpressionAttribute
    #pragma warning restore DesignedForInheritance
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RegexAttribute" /> class.
    /// </summary>
    protected RegexAttribute(string pattern) //
        : base(pattern) { }
}