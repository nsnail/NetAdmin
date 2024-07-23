using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色表
/// </summary>
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(Name), nameof(Name), true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_Role))]
public record Sys_Role : VersionEntity, IFieldSort, IFieldEnabled, IFieldSummary, IRegister
{
    /// <summary>
    ///     角色-接口映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleApi))]
    public ICollection<Sys_Api> Apis { get; init; }

    /// <summary>
    ///     仪表板布局
    /// </summary>
    #if DBTYPE_SQLSERVER
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_MAX)]
    #else
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    #endif
    [CsvIgnore]
    [JsonIgnore]
    public virtual string DashboardLayout { get; set; }

    /// <summary>
    ///     数据范围
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual DataScopes DataScope { get; init; }

    /// <summary>
    ///     角色-部门映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleDept))]
    public ICollection<Sys_Dept> Depts { get; init; }

    /// <summary>
    ///     是否显示仪表板
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool DisplayDashboard { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool IgnorePermissionControl { get; init; }

    /// <summary>
    ///     角色-菜单映射
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleMenu))]
    public ICollection<Sys_Menu> Menus { get; init; }

    /// <summary>
    ///     角色名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     发送给此角色的站内信集合
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_SiteMsgRole))]
    public ICollection<Sys_SiteMsg> SiteMsgs { get; init; }

    /// <summary>
    ///     排序值，越大越前
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long Sort { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; init; }

    /// <summary>
    ///     此角色下的用户集合
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_UserRole))]
    public ICollection<Sys_User> Users { get; init; }

    /// <inheritdoc />
    public virtual void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<CreateRoleReq, Sys_Role>()
                  .Map( //
                      d => d.Depts
                    , s => s.DeptIds.NullOrEmpty()
                          ? Array.Empty<Sys_Dept>()
                          : s.DeptIds.Select(x => new Sys_Dept { Id = x }))
                  .Map( //
                      d => d.Menus
                    , s => s.MenuIds.NullOrEmpty()
                          ? Array.Empty<Sys_Menu>()
                          : s.MenuIds.Select(x => new Sys_Menu { Id = x }))
                  .Map( //
                      d => d.Apis
                    , s => s.ApiIds.NullOrEmpty()
                          ? Array.Empty<Sys_Api>()
                          : s.ApiIds.Select(x => new Sys_Api { Id = x }))

            //
            ;
    }
}