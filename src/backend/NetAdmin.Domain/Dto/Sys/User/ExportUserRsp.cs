using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     响应：导出用户
/// </summary>
public record ExportUserRsp : QueryUserRsp
{
    /// <inheritdoc />
    [CsvIndex(7)]
    [Ignore(false)]
    [Name(nameof(Ln.创建时间))]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override QueryDeptRsp Dept { get; init; }

    /// <summary>
    ///     所属部门
    /// </summary>
    [CsvIndex(5)]
    [Name(nameof(Ln.所属部门))]
    public string DeptName { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [Ignore(false)]
    [Name(nameof(Ln.邮箱号))]
    public override string Email { get; init; }

    /// <inheritdoc />
    [CsvIndex(6)]
    [Ignore(false)]
    [Name(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [Ignore(false)]
    [Name(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(2)]
    [Ignore(false)]
    [Name(nameof(Ln.手机号))]
    public override string Mobile { get; init; }

    /// <summary>
    ///     所属角色
    /// </summary>
    [CsvIndex(4)]
    [Name(nameof(Ln.所属角色))]
    public string RoleNames { get; init; }

    /// <inheritdoc />
    [Ignore]
    public override IEnumerable<QueryRoleRsp> Roles { get; init; }

    /// <inheritdoc />
    [CsvIndex(1)]
    [Ignore(false)]
    [Name(nameof(Ln.用户名))]
    public override string UserName { get; init; }

    /// <inheritdoc />
    public override void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_User, ExportUserRsp>()
                  .Map(d => d.DeptName,  s => s.Dept.Name)
                  .Map(d => d.RoleNames, s => string.Join(',', s.Roles.Select(x => x.Name)));
    }
}