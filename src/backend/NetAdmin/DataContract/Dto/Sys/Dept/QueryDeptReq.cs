using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.Dto.Sys.Dept;

/// <summary>
///     请求：查询部门
/// </summary>
public record QueryDeptReq : TbSysDept
{
    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}