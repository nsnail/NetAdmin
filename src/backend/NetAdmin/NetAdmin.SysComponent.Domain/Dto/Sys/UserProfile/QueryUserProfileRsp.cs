using NetAdmin.SysComponent.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.UserProfile;

/// <summary>
///     响应：查询用户档案
/// </summary>
public sealed record QueryUserProfileRsp : Sys_UserProfile
{
    /// <inheritdoc cref="Sys_UserProfile.AppConfig" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string AppConfig { get; set; }

    /// <inheritdoc cref="Sys_UserProfile.BornDate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? BornDate { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CertificateNumber" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CertificateNumber { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CertificateType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override CertificateTypes? CertificateType { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CompanyAddress { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyArea" />
    public new QueryDicContentRsp CompanyArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CompanyName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CompanyTelephone { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Education" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Educations? Education { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactAddress { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactArea" />
    public new QueryDicContentRsp EmergencyContactArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactMobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactMobile { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.GraduateSchool" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string GraduateSchool { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Height" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override int? Height { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string HomeAddress { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeArea" />
    public new QueryDicContentRsp HomeArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string HomeTelephone { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.MarriageStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override MarriageStatues? MarriageStatus { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Nation" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Nations? Nation { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.NationArea" />
    public new QueryDicContentRsp NationArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.PoliticalStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override PoliticalStatues? PoliticalStatus { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Profession" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Profession { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.RealName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RealName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Sex" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Sexes? Sex { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}