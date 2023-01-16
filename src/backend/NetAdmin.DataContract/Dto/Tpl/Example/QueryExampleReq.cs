using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Tpl;

namespace NetAdmin.DataContract.Dto.Tpl.Example;

/// <summary>
///     请求：查询示例
/// </summary>
public record QueryExampleReq : TbTplExample
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}