namespace NetAdmin.Domain.Dto.Sys.Api;

/// <summary>
///     响应：导出接口
/// </summary>
public record ExportApiRsp : QueryApiRsp
{
    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryApiRsp> Children { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.接口路径))]
    public override string Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(2)]
    [CsvName(nameof(Ln.请求方式))]
    public override string Method { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.接口名称))]
    public override string Name { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.接口描述))]
    public override string Summary { get; init; }
}