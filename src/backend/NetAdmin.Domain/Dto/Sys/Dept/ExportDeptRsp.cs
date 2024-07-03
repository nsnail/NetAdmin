namespace NetAdmin.Domain.Dto.Sys.Dept;

/// <summary>
///     响应：导出部门
/// </summary>
public record ExportDeptRsp : QueryDeptRsp
{
    /// <inheritdoc />
    [Ignore]
    public override IEnumerable<QueryDeptRsp> Children { get; init; }

    /// <inheritdoc />
    [CsvIndex(5)]
    [Ignore(false)]
    [Name(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIndex(4)]
    [Ignore(false)]
    [Name(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [Ignore(false)]
    [Name(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [Ignore(false)]
    [Name(nameof(Ln.部门名称))]
    public override string Name { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [Ignore(false)]
    [Name(nameof(Ln.排序))]
    public override long Sort { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [Ignore(false)]
    [Name(nameof(Ln.备注))]
    public override string Summary { get; init; }
}