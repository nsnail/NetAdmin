namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     婚姻状况
/// </summary>
[Export]
public enum MarriageStatues
{
    /// <summary>
    ///     未婚
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Unmarried))]
    Unmarried = 1

   ,

    /// <summary>
    ///     已婚
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Married))]
    Married = 2

   ,

    /// <summary>
    ///     离异
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Divorced))]
    Divorced = 3

   ,

    /// <summary>
    ///     丧偶
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Berefted))]
    Bereft = 4
}