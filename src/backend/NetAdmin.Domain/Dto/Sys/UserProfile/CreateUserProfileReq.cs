using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     请求：创建用户档案
/// </summary>
public record CreateUserProfileReq : Sys_UserProfile
{
    /// <inheritdoc cref="Sys_UserProfile.BornDate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime? BornDate { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CertificateNumber" />
    [Certificate]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string CertificateNumber { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CertificateType" />
    [EnumDataType(typeof(CertificateTypes), ErrorMessageResourceType = typeof(Ln)
                , ErrorMessageResourceName = nameof(Ln.证件类型不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
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
    [Telephone]
    public override string CompanyTelephone { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Education" />
    [EnumDataType(typeof(Educations), ErrorMessageResourceType = typeof(Ln)
                , ErrorMessageResourceName = nameof(Ln.学历不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Educations? Education { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactAddress { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactArea" />
    public new QueryDicContentRsp EmergencyContactArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactMobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Mobile]
    public override string EmergencyContactMobile { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string EmergencyContactName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.GraduateSchool" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string GraduateSchool { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Height" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Range(100, 250)]
    public override int? Height { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string HomeAddress { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeArea" />
    public new QueryDicContentRsp HomeArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeTelephone" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Telephone]
    public override string HomeTelephone { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.MarriageStatus" />
    [EnumDataType(typeof(MarriageStatues), ErrorMessageResourceType = typeof(Ln)
                , ErrorMessageResourceName = nameof(Ln.婚姻状况不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override MarriageStatues? MarriageStatus { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Nation" />
    [EnumDataType(typeof(Nations), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.民族不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Nations? Nation { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.NationArea" />
    public new CreateDicContentReq NationArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.PoliticalStatus" />
    [EnumDataType(typeof(PoliticalStatues), ErrorMessageResourceType = typeof(Ln)
                , ErrorMessageResourceName = nameof(Ln.政治面貌不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override PoliticalStatues? PoliticalStatus { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Profession" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Profession { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.RealName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string RealName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Sex" />
    [EnumDataType(typeof(Sexes), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.性别不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Sexes? Sex { get; init; }
}