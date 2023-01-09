using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Dept;

/// <summary>
///     请求：创建部门
/// </summary>
public record CreateDeptReq : TbSysDept
{
    /// <inheritdoc cref="TbSysDept.BitSet" />
    public override long BitSet => (long)Enums.SysDeptBits.Enabled;

    /// <inheritdoc cref="TbSysDept.Label" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Label { get; set; }

    /// <inheritdoc cref="TbSysDept.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; set; }

    /// <inheritdoc cref="TbSysDept.Remark" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Remark { get; set; }

    /// <inheritdoc cref="TbSysDept.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Sort { get; set; }
}