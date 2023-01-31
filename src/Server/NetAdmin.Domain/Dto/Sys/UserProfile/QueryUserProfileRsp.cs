using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     响应：查询用户档案
/// </summary>
public record QueryUserProfileRsp : TbSysUserProfile
{
    /// <inheritdoc cref="TbSysUserProfile.BornDate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override DateTime? BornDate { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CertificateNumber" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CertificateNumber { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CertificateType" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Enums.CertificateTypes? CertificateType { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CompanyAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CompanyAddress { get; init; }

    /// <summary>
    ///     工作地区
    /// </summary>
    public new QueryDicContentRsp CompanyArea { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CompanyName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CompanyName { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.CompanyTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CompanyTelephone { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Education" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Enums.Educations? Education { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.EmergencyContactAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactAddress { get; init; }

    /// <summary>
    ///     紧急联系地区
    /// </summary>
    public new QueryDicContentRsp EmergencyContactArea { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.EmergencyContactMobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactMobile { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.EmergencyContactName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactName { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.GraduateSchool" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string GraduateSchool { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Height" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override int? Height { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.HomeAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string HomeAddress { get; init; }

    /// <summary>
    ///     住宅地区
    /// </summary>
    public new QueryDicContentRsp HomeArea { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.HomeTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string HomeTelephone { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.MarriageStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Enums.MarriageStatues? MarriageStatus { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Nation" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Enums.Nations? Nation { get; init; }

    /// <summary>
    ///     籍贯
    /// </summary>
    public new QueryDicContentRsp NationArea { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.PoliticalStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Enums.PoliticalStatues? PoliticalStatus { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Profession" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Profession { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.RealName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RealName { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Sex" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override Enums.Sexes? Sex { get; init; }

    /// <inheritdoc cref="TbSysUserProfile.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}