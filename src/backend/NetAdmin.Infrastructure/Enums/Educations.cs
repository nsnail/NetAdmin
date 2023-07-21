namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     学历
/// </summary>
[Export]
public enum Educations
{
    /// <summary>
    ///     小学
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.小学))]
    Primary = 1

   ,

    /// <summary>
    ///     初中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.初中))]
    Junior = 2

   ,

    /// <summary>
    ///     高中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.高中))]
    Higher = 3

   ,

    /// <summary>
    ///     中专
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.中专))]
    Technical = 4

   ,

    /// <summary>
    ///     大专
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.大专))]
    College = 5

   ,

    /// <summary>
    ///     本科
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.本科))]
    Bachelor = 6

   ,

    /// <summary>
    ///     硕士
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.硕士))]
    Master = 7

   ,

    /// <summary>
    ///     博士
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.博士))]
    Doctor = 8

   ,

    /// <summary>
    ///     博士后
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.博士后))]
    Post = 9
}