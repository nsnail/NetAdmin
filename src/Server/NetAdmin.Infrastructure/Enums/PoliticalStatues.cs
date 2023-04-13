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
    [ResourceDescription<Ln>(nameof(Ln.Member_of_the_Communist_Party_of_China))]
    MemberOfCommunistParty = 1

   ,

    /// <summary>
    ///     共青团员
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Communist_youth_league_member))]
    MemberOfCommunistYouthLeague = 2

   ,

    /// <summary>
    ///     群众
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.The_masses))]
    CommonPeople = 3
}