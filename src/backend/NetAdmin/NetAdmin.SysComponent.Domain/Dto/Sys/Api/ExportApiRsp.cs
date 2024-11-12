namespace NetAdmin.SysComponent.Domain.Dto.Sys.Api;

/// <summary>
///     响应：导出接口
/// </summary>
public sealed record ExportApiRsp : QueryApiRsp
{
    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryApiRsp> Children { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.接口路径))]
    public override string Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.请求方式))]
    public override string Method { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.接口名称))]
    public override string Name { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.接口描述))]
    public override string Summary { get; init; }
}