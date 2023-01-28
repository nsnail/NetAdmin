using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     请求：创建用户档案
/// </summary>
public record CreateUserProfileReq : TbSysUserProfile
{
    /// <inheritdoc cref="TbSysUserProfile.BornDate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime? BornDate { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CertificateNumber" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CertificateNumber { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CertificateType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Enums.CertificateTypes? CertificateType { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CompanyAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CompanyAddress { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CompanyName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CompanyName { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CompanyTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CompanyTelephone { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Education" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Enums.Educations? Education { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.EmergencyContactAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmergencyContactAddress { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.EmergencyContactMobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmergencyContactMobile { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.EmergencyContactName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmergencyContactName { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.GraduateSchool" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string GraduateSchool { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Height" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int? Height { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.HomeAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string HomeAddress { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.HomeTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string HomeTelephone { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.MarriageStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Enums.MarriageStatues? MarriageStatus { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Nation" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Enums.Nations? Nation { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.NationPlace" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int? NationPlace { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.PoliticalStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Enums.PoliticalStatues? PoliticalStatus { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Profession" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Profession { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.RealName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string RealName { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Sex" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Enums.Sexes? Sex { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Summary { get; init; }
}