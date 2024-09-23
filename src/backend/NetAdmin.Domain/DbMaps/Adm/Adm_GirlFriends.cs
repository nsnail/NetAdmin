namespace NetAdmin.Domain.DbMaps.Adm;

/// <summary>
///     女朋友表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Adm_GirlFriends))]
public record Adm_GirlFriends : VersionEntity
{
    /// <summary>
    /// 姓名
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string RealName { get; init; }

    /// <summary>
    /// 年龄
    /// </summary>
    [Column]
    public virtual int Age { get; init; }

    /// <summary>
    /// 身高，单位厘米
    /// </summary>
    [Column]
    public virtual double Height { get; init; }

    /// <summary>
    /// 爱好
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    public virtual string Hobby { get; init; }
}