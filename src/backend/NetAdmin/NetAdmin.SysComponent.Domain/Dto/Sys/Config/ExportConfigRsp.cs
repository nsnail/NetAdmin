using NetAdmin.SysComponent.Domain.Dto.Sys.Dept;
using NetAdmin.SysComponent.Domain.Dto.Sys.Role;

namespace NetAdmin.SysComponent.Domain.Dto.Sys.Config;

/// <summary>
///     响应：导出配置
/// </summary>
public sealed record ExportConfigRsp : QueryConfigRsp, IRegister
{
    /// <inheritdoc />
    [CsvIndex(6)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.是否启用))]
    public override bool Enabled { get; init; }

    /// <inheritdoc />
    [CsvIndex(0)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc />
    [CsvIndex(3)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.人工审核))]
    public override bool UserRegisterConfirm { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryDeptRsp UserRegisterDept { get; init; }

    /// <summary>
    ///     默认部门
    /// </summary>
    [CsvIndex(1)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.默认部门))]
    public string UserRegisterDeptName { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryRoleRsp UserRegisterRole { get; init; }

    /// <summary>
    ///     默认角色
    /// </summary>
    [CsvIndex(2)]
    [CsvIgnore(false)]
    [CsvName(nameof(Ln.默认角色))]
    public string UserRegisterRoleName { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_Config, ExportConfigRsp>()
                  .Map(d => d.UserRegisterDeptName, s => s.UserRegisterDept.Name)
                  .Map(d => d.UserRegisterRoleName, s => s.UserRegisterRole.Name);
    }
}