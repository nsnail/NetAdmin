using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：导出用户
/// </summary>
public record ExportUserRsp : QueryUserRsp
{
    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(7)]
    [CsvName(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryDeptRsp Dept { get; init; }

    /// <summary>
    ///     所属部门
    /// </summary>
    [CsvIndex(5)]
    [CsvName(nameof(Ln.所属部门))]
    public string DeptName { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(3)]
    [CsvName(nameof(Ln.邮箱号))]
    public override string Email { get; init; }

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
    [CsvIndex(2)]
    [CsvName(nameof(Ln.手机号))]
    public override string Mobile { get; init; }

    /// <summary>
    ///     所属角色
    /// </summary>
    [CsvIndex(4)]
    [CsvName(nameof(Ln.所属角色))]
    public string RoleNames { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc />
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.用户名))]
    public override string UserName { get; init; }

    /// <inheritdoc />
    public override void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_User, ExportUserRsp>()
                  .Map(d => d.DeptName,  s => s.Dept.Name)
                  .Map(d => d.RoleNames, s => string.Join(',', s.Roles.Select(x => x.Name)));
    }
}