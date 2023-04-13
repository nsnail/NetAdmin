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
    [ResourceDescription<Ln>(nameof(Ln.Identity_card))]
    IdentityCard = 1

   ,

    /// <summary>
    ///     护照
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Passport))]
    Passport = 2

   ,

    /// <summary>
    ///     外国人居留证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Alien_residence_permit))]
    ForeignerResidencePermit = 3

   ,

    /// <summary>
    ///     港澳台通行证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Hong_Kong_Macao_and_Taiwan_Pass))]
    HKorMacauPermit = 4

   ,

    /// <summary>
    ///     出生证
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Birth_certificate))]
    BirthCertificate = 5
}