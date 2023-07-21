using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户档案表
/// </summary>
[Table(Name = "Sys_UserProfile")]
public record Sys_UserProfile : VersionEntity, IFieldSummary, IRegister
{
    /// <summary>
    ///     出生日期
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual DateTime? BornDate { get; init; }

    /// <summary>
    ///     证件号码
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    public virtual string CertificateNumber { get; init; }

    /// <summary>
    ///     证件类型
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual CertificateTypes? CertificateType { get; init; }

    /// <summary>
    ///     工作地址
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string CompanyAddress { get; init; }

    /// <summary>
    ///     工作地区
    /// </summary>
    [JsonIgnore]
    [Column]
    public int? CompanyArea { get; init; }

    /// <summary>
    ///     工作单位
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string CompanyName { get; init; }

    /// <summary>
    ///     工作电话
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string CompanyTelephone { get; init; }

    /// <summary>
    ///     文化程度
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual Educations? Education { get; init; }

    /// <summary>
    ///     紧急联系地址
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string EmergencyContactAddress { get; init; }

    /// <summary>
    ///     紧急联系地区
    /// </summary>
    [JsonIgnore]
    [Column]
    public int? EmergencyContactArea { get; init; }

    /// <summary>
    ///     紧急联系人手机号
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    public virtual string EmergencyContactMobile { get; init; }

    /// <summary>
    ///     紧急联系人
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string EmergencyContactName { get; init; }

    /// <summary>
    ///     毕业学校
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string GraduateSchool { get; init; }

    /// <summary>
    ///     身高
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual int? Height { get; init; }

    /// <summary>
    ///     住宅地址
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    public virtual string HomeAddress { get; init; }

    /// <summary>
    ///     住宅地区
    /// </summary>
    [JsonIgnore]
    [Column]
    public int? HomeArea { get; init; }

    /// <summary>
    ///     住宅电话
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string HomeTelephone { get; init; }

    /// <summary>
    ///     婚姻状况
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual MarriageStatues? MarriageStatus { get; init; }

    /// <summary>
    ///     民族
    /// </summary>
    /// 7
    [JsonIgnore]
    [Column]
    public virtual Nations? Nation { get; init; }

    /// <summary>
    ///     籍贯
    /// </summary>
    [JsonIgnore]
    [Column]
    public int? NationArea { get; init; }

    /// <summary>
    ///     政治面貌
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual PoliticalStatues? PoliticalStatus { get; init; }

    /// <summary>
    ///     职业
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string Profession { get; init; }

    /// <summary>
    ///     真实姓名
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    public virtual string RealName { get; init; }

    /// <summary>
    ///     性别
    /// </summary>
    [JsonIgnore]
    [Column]
    public virtual Sexes? Sex { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     用户基本信息
    /// </summary>
    [JsonIgnore]
    public Sys_User User { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateUserProfileReq, Sys_UserProfile>()
                  .Map(d => d.NationArea,  s => s.NationArea  == null ? null : s.NationArea.Value)
                  .Map(d => d.CompanyArea, s => s.CompanyArea == null ? null : s.CompanyArea.Value)
                  .Map(d => d.HomeArea,    s => s.HomeArea    == null ? null : s.HomeArea.Value)
                  .Map( //
                      d => d.EmergencyContactArea
                    , s => s.EmergencyContactArea == null ? null : s.EmergencyContactArea.Value);
    }
}