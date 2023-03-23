using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     请求：查询配置
/// </summary>
public record QueryConfigReq : TbSysConfig
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool? Enabled { get; set; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}