using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.Aop.Attributes.DataValidation;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：创建角色
/// </summary>
public record CreateRoleReq : TbSysRole, IRegister
{
    /// <inheritdoc cref="TbSysRole.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)Enums.SysRoleBits.Enabled;
            }

            if (IgnorePermissionControl) {
                ret |= (long)Enums.SysRoleBits.IgnorePermissionControl;
            }

            return ret;
        }
    }

    /// <inheritdoc cref="TbSysRole.DataScope" />
    [EnumDataType(typeof(Enums.DataScopes))]
    public override Enums.DataScopes DataScope { get; set; } = Enums.DataScopes.All;

    /// <summary>
    ///     当 DataScope = SpecificDept ，此参数指定部门id
    /// </summary>
    [SpecificDept]
    public IReadOnlyCollection<long> DeptIds { get; set; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    public bool IgnorePermissionControl { get; set; }

    /// <inheritdoc cref="TbSysRole.Label" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Label { get; set; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    public IReadOnlyCollection<long> MenuIds { get; set; }

    /// <inheritdoc cref="TbSysRole.Remark" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Remark { get; set; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Sort { get; set; }

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

            //
            ;
    }
}