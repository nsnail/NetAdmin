namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     响应：导出字典内容
/// </summary>
public sealed record ExportDicContentRsp : QueryDicContentRsp
{
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
    [CsvName(nameof(Ln.项名))]
    public override string Key { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.备注))]
    public override string Summary { get; set; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.项值))]
    public override string Value { get; init; }
}