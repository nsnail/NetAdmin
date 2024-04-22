using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Tpl;

/// <summary>
///     示例表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Tpl_Example))]
public record Tpl_Example : VersionEntity;