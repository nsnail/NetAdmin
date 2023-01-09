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
    public QueryDeptRsp Dept { get; set; }

    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="TbSysUser.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Mobile { get; set; }

    /// <summary>
    ///     角色列表
    /// </summary>
    public List<QueryRoleRsp> Roles { get; set; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserName { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; set; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Tuple<TbSysUser, TbSysDept, IEnumerable<TbSysRole>>, QueryUserRsp>()
              .Map(dest => dest.BitSet,      src => src.Item1.BitSet)
              .Map(dest => dest.Version,     src => src.Item1.Version)
              .Map(dest => dest.Avatar,      src => src.Item1.Avatar)
              .Map(dest => dest.CreatedTime, src => src.Item1.CreatedTime.Is(default, DateTime.Now))
              .Map(dest => dest.Dept,        src => src.Item2.Adapt<QueryDeptRsp>())
              .Map(dest => dest.Id,          src => src.Item1.Id)
              .Map(dest => dest.Mobile,      src => src.Item1.Mobile)
              .Map(dest => dest.Roles,       src => src.Item3.Select(x => x.Adapt<QueryRoleRsp>()))
              .Map(dest => dest.UserName,    src => src.Item1.UserName);
    }
}