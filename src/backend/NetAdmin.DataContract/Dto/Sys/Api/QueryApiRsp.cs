using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;

namespace NetAdmin.DataContract.Dto.Sys.Api;

/// <summary>
///     响应：查询接口
/// </summary>
public record QueryApiRsp : TbSysApi
{
    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public new IEnumerable<QueryApiRsp> Children { get; init; }

    /// <inheritdoc />
    public override string Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Method { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Name { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Namespace { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ParentId { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Summary { get; init; }
}