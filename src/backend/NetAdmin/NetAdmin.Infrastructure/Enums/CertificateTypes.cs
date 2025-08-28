namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     证件类型
/// </summary>
[Export]
public enum CertificateTypes
{
    /// <summary>
    ///     身份证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.身份证))]
    IdentityCard = 1

    ,

    /// <summary>
    ///     护照
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.护照))]
    Passport = 2

    ,

    /// <summary>
    ///     外国人居留证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.外国人居留证))]
    ForeignerResidencePermit = 3

    ,

    /// <summary>
    ///     港澳台通行证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.港澳台通行证))]
    HKorMacauPermit = 4

    ,

    /// <summary>
    ///     出生证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.出生证))]
    BirthCertificate = 5
}