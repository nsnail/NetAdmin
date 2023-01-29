using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     响应：查询角色
/// </summary>
public record QueryRoleRsp : TbSysRole, IRegister
{
    /// <summary>
    ///     是否显示仪表板
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool DisplayDashboard => BitSet.HasFlag(RoleBits.DisplayDashboard);

    /// <summary>
    ///     是否启用
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IgnorePermissionControl => BitSet.HasFlag(RoleBits.IgnorePermissionControl);

    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IReadOnlyCollection<string> ApiIds { get; init; }

    /// <inheritdoc cref="IFieldAdd.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="TbSysRole.DataScope" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override DataScopes DataScope { get; init; }

    /// <summary>
    ///     角色-部门映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IEnumerable<long> DeptIds { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public IReadOnlyCollection<long> MenuIds { get; init; }

    /// <inheritdoc cref="TbSysRole.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; }

    /// <inheritdoc cref="TbSysRole.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TbSysRole, QueryRoleRsp>()
              .IgnoreIf((src, dest) => src.Depts == null, dest => dest.DeptIds)
              .IgnoreIf((src, dest) => src.Menus == null, dest => dest.MenuIds)
              .IgnoreIf((src, dest) => src.Apis  == null, dest => dest.ApiIds)
              .Map(dest => dest.DeptIds, src => src.Depts.Select(x => x.Id))
              .Map(dest => dest.ApiIds,  src => src.Apis.Select(x => x.Id))
              .Map(dest => dest.MenuIds, src => src.Menus.Select(x => x.Id))

            //
            ;
    }
}