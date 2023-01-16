using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.Dept;

/// <summary>
///     请求：查询部门
/// </summary>
public record QueryDeptReq : TbSysDept
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}