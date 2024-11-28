using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户档案表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_UserProfile))]
public record Sys_UserProfile : VersionEntity, IRegister
{
    /// <summary>
    ///     应用配置
    /// </summary>
    [Column(DbType = Chars.FLGL_DB_FIELD_TYPE_VARCHAR_MAX)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string AppConfig { get; set; }

    /// <summary>
    ///     出生日期
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual DateTime? BornDate { get; init; }

    /// <summary>
    ///     证件号码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CertificateNumber { get; init; }

    /// <summary>
    ///     证件类型
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual CertificateTypes? CertificateType { get; init; }

    /// <summary>
    ///     工作地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CompanyAddress { get; init; }

    /// <summary>
    ///     工作地区
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public int? CompanyArea { get; init; }

    /// <summary>
    ///     工作单位
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CompanyName { get; init; }

    /// <summary>
    ///     工作电话
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string CompanyTelephone { get; init; }

    /// <summary>
    ///     文化程度
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual Educations? Education { get; init; }

    /// <summary>
    ///     紧急联系地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string EmergencyContactAddress { get; init; }

    /// <summary>
    ///     紧急联系地区
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public int? EmergencyContactArea { get; init; }

    /// <summary>
    ///     紧急联系人手机号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string EmergencyContactMobile { get; init; }

    /// <summary>
    ///     紧急联系人
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string EmergencyContactName { get; init; }

    /// <summary>
    ///     毕业学校
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string GraduateSchool { get; init; }

    /// <summary>
    ///     身高
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int? Height { get; init; }

    /// <summary>
    ///     住宅地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string HomeAddress { get; init; }

    /// <summary>
    ///     住宅地区
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public int? HomeArea { get; init; }

    /// <summary>
    ///     住宅电话
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string HomeTelephone { get; init; }

    /// <summary>
    ///     婚姻状况
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual MarriageStatues? MarriageStatus { get; init; }

    /// <summary>
    ///     民族
    /// </summary>
    /// 7
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual Nations? Nation { get; init; }

    /// <summary>
    ///     籍贯
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public int? NationArea { get; init; }

    /// <summary>
    ///     政治面貌
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual PoliticalStatues? PoliticalStatus { get; init; }

    /// <summary>
    ///     职业
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Profession { get; init; }

    /// <summary>
    ///     真实姓名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string RealName { get; init; }

    /// <summary>
    ///     性别
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual Sexes? Sex { get; init; }

    /// <summary>
    ///     用户基本信息
    /// </summary>
    [CsvIgnore]
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
                      d => d.EmergencyContactArea, s => s.EmergencyContactArea == null ? null : s.EmergencyContactArea.Value);
    }
}