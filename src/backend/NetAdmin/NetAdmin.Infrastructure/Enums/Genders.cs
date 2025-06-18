namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     性别
/// </summary>
[Export]
public enum Genders
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
    ///     密
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.密))]
    Secrecy = 3
}