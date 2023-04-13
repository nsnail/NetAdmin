using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.Domain.Dto.Sys.UserProfile;

/// <summary>
///     请求：创建用户档案
/// </summary>
public record CreateUserProfileReq : Sys_UserProfile, IRegister
{
    /// <inheritdoc cref="Sys_UserProfile.BornDate" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime? BornDate { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CertificateNumber" />
    [Certificate]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CertificateNumber { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CertificateType" />
    [EnumDataType(typeof(CertificateTypes))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override CertificateTypes? CertificateType { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CompanyAddress { get; init; }

    /// <summary>
    ///     工作地区
    /// </summary>
    public new QueryDicContentRsp CompanyArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CompanyName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.CompanyTelephone" />
    [Telephone]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CompanyTelephone { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Education" />
    [EnumDataType(typeof(Educations))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Educations? Education { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmergencyContactAddress { get; init; }

    /// <summary>
    ///     紧急联系地区
    /// </summary>
    public new QueryDicContentRsp EmergencyContactArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactMobile" />
    [Mobile]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmergencyContactMobile { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.EmergencyContactName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string EmergencyContactName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.GraduateSchool" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string GraduateSchool { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Height" />
    [Range(100, 250)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int? Height { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeAddress" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string HomeAddress { get; init; }

    /// <summary>
    ///     住宅地区
    /// </summary>
    public new QueryDicContentRsp HomeArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.HomeTelephone" />
    [Telephone]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string HomeTelephone { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.MarriageStatus" />
    [EnumDataType(typeof(MarriageStatues))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override MarriageStatues? MarriageStatus { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Nation" />
    [EnumDataType(typeof(Nations))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Nations? Nation { get; init; }

    /// <summary>
    ///     籍贯
    /// </summary>
    public new CreateDicContentReq NationArea { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.PoliticalStatus" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [EnumDataType(typeof(PoliticalStatues))]
    public override PoliticalStatues? PoliticalStatus { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Profession" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Profession { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.RealName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string RealName { get; init; }

    /// <inheritdoc cref="Sys_UserProfile.Sex" />
    [EnumDataType(typeof(Sexes))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Sexes? Sex { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IRegister.Register" />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateUserProfileReq, Sys_UserProfile>()
                  .Map( //
                      dest => dest.NationArea, src => src.NationArea == null ? null : src.NationArea.Value)
                  .Map( //
                      dest => dest.CompanyArea, src => src.CompanyArea == null ? null : src.CompanyArea.Value)
                  .Map( //
                      dest => dest.HomeArea, src => src.HomeArea == null ? null : src.HomeArea.Value)
                  .Map( //
                      dest => dest.EmergencyContactArea
                    , src => src.EmergencyContactArea == null ? null : src.EmergencyContactArea.Value);
    }
}