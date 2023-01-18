using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：创建角色
/// </summary>
public record CreateRoleReq : TbSysRole, IRegister
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    public IReadOnlyCollection<string> ApiIds { get; init; }

    /// <inheritdoc cref="TbSysRole.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)BitSets.Enabled;
            }

            if (IgnorePermissionControl) {
                ret |= (long)RoleBits.IgnorePermissionControl;
            }

            return ret;
        }
    }

    /// <inheritdoc cref="TbSysRole.DataScope" />
    [EnumDataType(typeof(DataScopes))]
    public override DataScopes DataScope { get; init; } = DataScopes.All;

    /// <summary>
    ///     当 DataScope = SpecificDept ，此参数指定部门id
    /// </summary>
    [SpecificDept]
    public IReadOnlyCollection<long> DeptIds { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    public bool IgnorePermissionControl { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    public IReadOnlyCollection<long> MenuIds { get; init; }

    /// <inheritdoc cref="TbSysRole.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Required]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override int Sort { get; init; }

    /// <inheritdoc cref="TbSysRole.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Summary { get; init; }

    /// <inheritdoc />
    public virtual void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateRoleReq, TbSysRole>()
              .Map( //
                  dest => dest.Depts
                , src => src.DeptIds.NullOrEmpty()
                      ? Array.Empty<TbSysDept>()
                      : src.DeptIds.Select(x => new TbSysDept { Id = x }))
              .Map( //
                  dest => dest.Menus
                , src => src.MenuIds.NullOrEmpty()
                      ? Array.Empty<TbSysMenu>()
                      : src.MenuIds.Select(x => new TbSysMenu { Id = x }))
              .Map( //
                  dest => dest.Apis
                , src => src.ApiIds.NullOrEmpty()
                      ? Array.Empty<TbSysApi>()
                      : src.ApiIds.Select(x => new TbSysApi { Id = x }))

            //
            ;
    }
}