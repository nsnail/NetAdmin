using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     岗位表
/// </summary>
[Table(Name = nameof(Sys_Position))]
[Index($"idx_{{tablename}}_{nameof(Name)}", nameof(Name), true)]
public record Sys_Position : VersionEntity, IFieldSort, IFieldSummary
{
    /// <summary>
    ///     岗位名称
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR31)]
    public virtual string Name { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    public virtual long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <summary>
    ///     岗位描述
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR255)]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     此岗位下的用户集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_UserPosition))]
    public ICollection<Sys_User> Users { get; init; }
}