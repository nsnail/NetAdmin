using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using Mapster;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.DataContract.Dto.Sys.User;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     用户表
/// </summary>
[Table]
public record TbSysUser : DefaultEntity, IFieldBitSet, IRegister
{
    /// <summary>
    ///     头像链接
    /// </summary>
    [JsonIgnore]
    public virtual string Avatar { get; set; }

    /// <summary>
    ///     比特位 <see cref="Enums.SysUserBits" />
    /// </summary>
    [JsonIgnore]
    public virtual long BitSet { get; set; }

    /// <summary>
    ///     部门id
    /// </summary>
    [JsonIgnore]
    public virtual long DeptId { get; set; }

    /// <summary>
    ///     手机号
    /// </summary>
    [JsonIgnore]
    public virtual string Mobile { get; set; }

    /// <summary>
    ///     密码
    /// </summary>
    [JsonIgnore]
    public virtual Guid Password { get; set; }

    /// <summary>
    ///     做授权验证的Token，全局唯一，可以随时重置（强制下线）
    /// </summary>
    [JsonIgnore]
    public virtual Guid Token { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    [JsonIgnore]
    public virtual string UserName { get; set; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateUserReq, TbSysUser>()
              .Map(dest => dest.Password, src => src.Password.Pwd().Guid())
              .Map(dest => dest.Token,    src => Guid.NewGuid())
              .Map(dest => dest.BitSet,   src => Enums.SysUserBits.Enabled);
    }
}