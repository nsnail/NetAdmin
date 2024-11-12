namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     政治面貌
/// </summary>
[Export]
public enum PoliticalStatues
{
    /// <summary>
    ///     中共党员
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.中共党员))]
    MemberOfCommunistParty = 1

   ,

    /// <summary>
    ///     共青团员
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.共青团员))]
    MemberOfCommunistYouthLeague = 2

   ,

    /// <summary>
    ///     群众
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.群众))]
    CommonPeople = 3
}