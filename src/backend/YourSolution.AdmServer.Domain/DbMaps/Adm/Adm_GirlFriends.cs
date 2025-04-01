using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace YourSolution.AdmServer.Domain.DbMaps.Adm;

/// <summary>
///     女朋友表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Adm_GirlFriends))]
public record Adm_GirlFriends : VersionEntity
{
    /// <summary>
    ///     姓名
    /// </summary>
    public virtual string Name { get; init; }

    /// <summary>
    ///     年龄
    /// </summary>
    public virtual int Age { get; init; }

    /// <summary>
    ///     身高
    /// </summary>
    public virtual int Height { get; init; }

    /// <summary>
    ///     爱好
    /// </summary>
    public virtual string Interest { get; init; }

    /// <summary>
    ///     种子
    /// </summary>
    public virtual string Seed { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [JsonIgnore]
    public virtual bool Enabled { get; init; }
}