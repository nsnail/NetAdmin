namespace NetAdmin.Domain.Dto.Sys.Doc.Content;

/// <summary>
///     响应：导出文档内容
/// </summary>
public sealed record ExportDocContentRsp : QueryDocContentRsp
{
    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.文档内容))]
    public override string Body { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.文档标题))]
    public override string Title { get; init; }
}