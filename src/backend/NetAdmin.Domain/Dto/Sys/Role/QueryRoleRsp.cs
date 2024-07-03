using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     响应：查询角色
/// </summary>
public record QueryRoleRsp : Sys_Role
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<string> ApiIds { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="Sys_Role.DashboardLayout" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string DashboardLayout { get; set; }

    /// <inheritdoc cref="Sys_Role.DataScope" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DataScopes DataScope { get; init; }

    /// <summary>
    ///     角色-部门映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<long> DeptIds { get; init; }

    /// <inheritdoc cref="Sys_Role.DisplayDashboard" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool DisplayDashboard { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Role.IgnorePermissionControl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool IgnorePermissionControl { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<long> MenuIds { get; init; }

    /// <inheritdoc cref="Sys_Role.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }

    /// <inheritdoc />
    public override void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_Role, QueryRoleRsp>() //
                  .IgnoreIf((s, _) => s.Depts == null, d => d.DeptIds)
                  .IgnoreIf((s, _) => s.Menus == null, d => d.MenuIds)
                  .IgnoreIf((s, _) => s.Apis  == null, d => d.ApiIds)
                  .Map(d => d.DeptIds, s => s.Depts.Select(x => x.Id))
                  .Map(d => d.ApiIds,  s => s.Apis.Select(x => x.Id))
                  .Map(d => d.MenuIds, s => s.Menus.Select(x => x.Id))

            //
            ;
    }
}