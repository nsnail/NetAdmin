using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：创建角色
/// </summary>
public record CreateRoleReq : Sys_Role
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    public IReadOnlyCollection<string> ApiIds { get; init; }

    /// <inheritdoc cref="Sys_Role.DataScope" />
    [EnumDataType(typeof(DataScopes))]
    public override DataScopes DataScope { get; init; } = DataScopes.All;

    /// <summary>
    ///     当 DataScope = SpecificDept ，此参数指定部门编号
    /// </summary>
    [SpecificDept]
    public IReadOnlyCollection<long> DeptIds { get; init; }

    /// <inheritdoc cref="Sys_Role.DisplayDashboard" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool DisplayDashboard { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="Sys_Role.IgnorePermissionControl" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool IgnorePermissionControl { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    public IReadOnlyCollection<long> MenuIds { get; init; }

    /// <inheritdoc cref="Sys_Role.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.角色名称))]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Summary { get; init; }
}