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
    [ResourceDescription<Ln>(nameof(Ln.顺序排序))]
    Ascending = 1

   ,

    /// <summary>
    ///     倒序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.倒序排序))]
    Descending = 2
}