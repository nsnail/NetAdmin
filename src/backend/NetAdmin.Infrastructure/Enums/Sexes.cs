namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     性别
/// </summary>
[Export]
public enum Sexes
{
    /// <summary>
    ///     男
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.男))]
    Male = 1

   ,

    /// <summary>
    ///     女
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.女))]
    Female = 2

   ,

    /// <summary>
    ///     保密
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.保密))]
    Secrecy = 3
}