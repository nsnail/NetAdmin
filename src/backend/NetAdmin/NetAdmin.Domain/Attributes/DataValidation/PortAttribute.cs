namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     端口号验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class PortAttribute : RangeAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PortAttribute" /> class.
    /// </summary>
    public PortAttribute() //
        : base(1, ushort.MaxValue)
    {
        ErrorMessageResourceName = nameof(Ln.无效端口号);
        ErrorMessageResourceType = typeof(Ln);
    }
}