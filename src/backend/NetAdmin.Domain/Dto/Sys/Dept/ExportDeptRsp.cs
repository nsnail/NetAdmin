namespace NetAdmin.Domain.Dto.Sys.Dept;

/// <summary>
///     响应：导出部门
/// </summary>
public sealed record ExportDeptRsp : QueryDeptRsp
{
    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryDeptRsp> Children { get; init; }

    /// <inheritdoc />
    [CsvIndex(5)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.部门名称))]
    public override string Name { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.排序))]
    public override long Sort { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.备注))]
    public override string Summary { get; init; }
}