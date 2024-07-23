using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Role;

namespace NetAdmin.Domain.Dto.Sys.Config;

/// <summary>
///     响应：导出配置
/// </summary>
public record ExportConfigRsp : QueryConfigRsp, IRegister
{
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
    [CsvName(nameof(Ln.人工审核))]
    public override bool UserRegisterConfirm { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryDeptRsp UserRegisterDept { get; init; }

    /// <summary>
    ///     默认部门
    /// </summary>
    [CsvIgnore(false)]
    [CsvIndex(1)]
    [CsvName(nameof(Ln.默认部门))]
    public string UserRegisterDeptName { get; init; }

    /// <inheritdoc />
    [CsvIgnore]
    public override QueryRoleRsp UserRegisterRole { get; init; }

    /// <summary>
    ///     默认角色
    /// </summary>
    [CsvIgnore(false)]
    [CsvIndex(2)]
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