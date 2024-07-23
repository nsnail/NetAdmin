namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     响应：导出字典内容
/// </summary>
public record ExportDicContentRsp : QueryDicContentRsp
{
    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(2)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.项名))]
    public override string Key { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.项值))]
    public override string Value { get; init; }
}