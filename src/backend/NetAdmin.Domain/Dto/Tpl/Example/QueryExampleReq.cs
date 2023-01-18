using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Tpl;

namespace NetAdmin.Domain.Dto.Tpl.Example;

/// <summary>
///     请求：查询示例
/// </summary>
public record QueryExampleReq : TbTplExample
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}