using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Position;

/// <summary>
///     响应：查询岗位
/// </summary>
public record QueryPositionRsp : TbSysPosition
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="TbSysPosition.Name" />
    [Required]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}