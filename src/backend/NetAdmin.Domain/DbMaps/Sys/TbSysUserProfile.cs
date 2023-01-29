using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户档案表
/// </summary>
[Table]
public record TbSysUserProfile : MutableEntity, IFieldSummary
{
    /// <summary>
    ///     出生日期
    /// </summary>
    [JsonIgnore]
    public virtual DateTime? BornDate { get; init; }

    /// <summary>
    ///     证件号码
    /// </summary>
    [JsonIgnore]
    [Certificate]
    [MaxLength(63)]
    public virtual string CertificateNumber { get; init; }

    /// <summary>
    ///     证件类型
    /// </summary>
    [JsonIgnore]
    [EnumDataType(typeof(Enums.CertificateTypes))]
    public virtual Enums.CertificateTypes? CertificateType { get; init; }

    /// <summary>
    ///     工作地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string CompanyAddress { get; init; }

    /// <summary>
    ///     工作地区
    /// </summary>
    [JsonIgnore]
    public virtual int? CompanyArea { get; init; }

    /// <summary>
    ///     工作单位
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string CompanyName { get; init; }

    /// <summary>
    ///     工作电话
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    [Telephone]
    public virtual string CompanyTelephone { get; init; }

    /// <summary>
    ///     文化程度
    /// </summary>
    [JsonIgnore]
    [EnumDataType(typeof(Enums.Educations))]
    public virtual Enums.Educations? Education { get; init; }

    /// <summary>
    ///     紧急联系地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string EmergencyContactAddress { get; init; }

    /// <summary>
    ///     紧急联系地区
    /// </summary>
    [JsonIgnore]
    public virtual int? EmergencyContactArea { get; init; }

    /// <summary>
    ///     紧急联系人手机号
    /// </summary>
    [Mobile]
    [JsonIgnore]
    [MaxLength(15)]
    public virtual string EmergencyContactMobile { get; init; }

    /// <summary>
    ///     紧急联系人
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string EmergencyContactName { get; init; }

    /// <summary>
    ///     毕业学校
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string GraduateSchool { get; init; }

    /// <summary>
    ///     身高
    /// </summary>
    [JsonIgnore]
    [Range(100, 250)]
    public virtual int? Height { get; init; }

    /// <summary>
    ///     住宅地址
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string HomeAddress { get; init; }

    /// <summary>
    ///     住宅地区
    /// </summary>
    [JsonIgnore]
    public virtual int? HomeArea { get; init; }

    /// <summary>
    ///     住宅电话
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    [Telephone]
    public virtual string HomeTelephone { get; init; }

    /// <summary>
    ///     婚姻状况
    /// </summary>
    [JsonIgnore]
    [EnumDataType(typeof(Enums.MarriageStatues))]
    public virtual Enums.MarriageStatues? MarriageStatus { get; init; }

    /// <summary>
    ///     民族
    /// </summary>
    /// 7
    [JsonIgnore]
    [EnumDataType(typeof(Enums.Nations))]
    public virtual Enums.Nations? Nation { get; init; }

    /// <summary>
    ///     籍贯
    /// </summary>
    [JsonIgnore]
    public virtual int? NationArea { get; init; }

    /// <summary>
    ///     政治面貌
    /// </summary>
    [JsonIgnore]
    [EnumDataType(typeof(Enums.PoliticalStatues))]
    public virtual Enums.PoliticalStatues? PoliticalStatus { get; init; }

    /// <summary>
    ///     职业
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Profession { get; init; }

    /// <summary>
    ///     真实姓名
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string RealName { get; init; }

    /// <summary>
    ///     性别
    /// </summary>
    [JsonIgnore]
    [EnumDataType(typeof(Enums.Sexes))]
    public virtual Enums.Sexes? Sex { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    [JsonIgnore]
    [MaxLength(255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     用户基本信息
    /// </summary>
    [JsonIgnore]
    public virtual TbSysUser User { get; init; }
}