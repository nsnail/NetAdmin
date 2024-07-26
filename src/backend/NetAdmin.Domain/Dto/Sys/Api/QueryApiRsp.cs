namespace NetAdmin.Domain.Dto.Sys.Api;

/// <summary>
///     响应：查询接口
/// </summary>
public record QueryApiRsp : Sys_Api
{
    /// <inheritdoc cref="Sys_Api.Children" />
    public new virtual IEnumerable<QueryApiRsp> Children { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    public override string Id { get; init; }

    /// <inheritdoc cref="Sys_Api.Method" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Method { get; init; }

    /// <inheritdoc cref="Sys_Api.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="Sys_Api.Namespace" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Namespace { get; init; }

    /// <inheritdoc cref="Sys_Api.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string ParentId { get; init; }

    /// <inheritdoc cref="Sys_Api.PathCrc32" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int PathCrc32 { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }
}