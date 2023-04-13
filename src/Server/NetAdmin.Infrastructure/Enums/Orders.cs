namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     排序方式
/// </summary>
[Export]
public enum Orders
{
    /// <summary>
    ///     顺序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Sort_in_order))]
    Ascending = 1

   ,

    /// <summary>
    ///     倒序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Sort_in_reverse_order))]
    Descending = 2
}