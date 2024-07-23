using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     响应：导出角色
/// </summary>
public sealed record ExportRoleRsp : QueryRoleRsp
{
    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(7)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(4)]
    [CsvName(nameof(Ln.数据范围))]
    public override DataScopes DataScope { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(5)]
    [CsvName(nameof(Ln.显示仪表板))]
    public override bool DisplayDashboard { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(6)]
    [CsvName(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(0)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.无限权限))]
    public override bool IgnorePermissionControl { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.角色名称))]
    public override string Name { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(2)]
    [CsvName(nameof(Ln.排序))]
    public override long Sort { get; init; }
}