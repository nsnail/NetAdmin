namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     排序方式
/// </summary>
[Export]
public enum Orders
{
    /// <summary>
    ///     顺序排序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.顺序排序))]
    Ascending = 1

    ,

    /// <summary>
    ///     倒序排序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.倒序排序))]
    Descending = 2

    ,

    /// <summary>
    ///     随机排序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.随机排序))]
    Random = 3

    ,

    /// <summary>
    ///     不排序
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.不排序))]
    None = 4
}