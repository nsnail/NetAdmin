namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     区间验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CultureRangeAttribute : RangeAttribute
{
    /// <inheritdoc cref="RangeAttribute" />
    public CultureRangeAttribute(double minimum, double maximum) //
        : base(minimum, maximum) { }

    /// <inheritdoc cref="RangeAttribute" />
    public CultureRangeAttribute(int minimum, int maximum) //
        : base(minimum, maximum) { }

    /// <inheritdoc cref="RangeAttribute" />
    public CultureRangeAttribute(Type type, string minimum, string maximum) //
        : base(type, minimum, maximum) { }

    /// <inheritdoc />
    public override string FormatErrorMessage(string name)
    {
        return $"{ErrorMessageString} {Ln.必须介于} {Minimum} - {Maximum}";
    }
}