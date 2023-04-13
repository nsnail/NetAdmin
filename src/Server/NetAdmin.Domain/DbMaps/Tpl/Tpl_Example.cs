using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Tpl;

/// <summary>
///     示例表
/// </summary>
[Table(Name = nameof(Tpl_Example))]
public record Tpl_Example : VersionEntity;