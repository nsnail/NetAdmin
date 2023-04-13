using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     响应：查询角色
/// </summary>
public sealed record QueryRoleRsp : Sys_Role, IRegister
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<string> ApiIds { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

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

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
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

    /// <inheritdoc cref="IRegister.Register" />
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<Sys_Role, QueryRoleRsp>()
                  .IgnoreIf((src, _) => src.Depts == null, dest => dest.DeptIds)
                  .IgnoreIf((src, _) => src.Menus == null, dest => dest.MenuIds)
                  .IgnoreIf((src, _) => src.Apis  == null, dest => dest.ApiIds)
                  .Map(dest => dest.DeptIds, src => src.Depts.Select(x => x.Id))
                  .Map(dest => dest.ApiIds,  src => src.Apis.Select(x => x.Id))
                  .Map(dest => dest.MenuIds, src => src.Menus.Select(x => x.Id))

            //
            ;
    }
}