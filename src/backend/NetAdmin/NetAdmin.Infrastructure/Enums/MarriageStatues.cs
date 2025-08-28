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
    [ResourceDescription<Ln>(nameof(Ln.未婚))]
    Unmarried = 1

    ,

    /// <summary>
    ///     已婚
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.已婚))]
    Married = 2

    ,

    /// <summary>
    ///     离异
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.离异))]
    Divorced = 3

    ,

    /// <summary>
    ///     丧偶
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.丧偶))]
    Bereft = 4
}