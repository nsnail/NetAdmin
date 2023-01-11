using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.DataContract.Dto.Sys.Dept;
using NetAdmin.DataContract.Dto.Sys.Role;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     响应：查询用户
/// </summary>
public record QueryUserRsp : TbSysUser, IRegister
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(Enums.SysUserBits.Enabled);

    /// <inheritdoc cref="TbSysUser.Avatar" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Avatar { get; set; }

    /// <inheritdoc cref="IFieldAdd.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; set; }

    /// <summary>
    ///     部门
    /// </summary>
    public new QueryDeptRsp Dept { get; set; }

    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="TbSysUser.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Mobile { get; set; }

    /// <summary>
    ///     角色列表
    /// </summary>
    public new IEnumerable<QueryRoleRsp> Roles { get; set; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserName { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; set; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TbSysUser, QueryUserRsp>()
              .Map(dest => dest.CreatedTime, src => src.CreatedTime.Is(default, DateTime.Now))
              .Map(dest => dest.Dept,        src => src.Dept.Adapt<QueryDeptRsp>())
              .Map(dest => dest.Roles,       src => src.Roles.Select(x => x.Adapt<QueryRoleRsp>()));
    }
}