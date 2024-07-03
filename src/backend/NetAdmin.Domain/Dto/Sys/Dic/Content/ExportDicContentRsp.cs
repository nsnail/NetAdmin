namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     响应：导出字典内容
/// </summary>
public record ExportDicContentRsp : QueryDicContentRsp
{
    /// <inheritdoc />
    [CsvIndex(2)]
    [Ignore(false)]
    [Name(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [Ignore(false)]
    [Name(nameof(Ln.项名))]
    public override string Key { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [Ignore(false)]
    [Name(nameof(Ln.项值))]
    public override string Value { get; init; }
}