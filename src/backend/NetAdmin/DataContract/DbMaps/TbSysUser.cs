using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     用户表
/// </summary>
[Table]
[Index($"idx_{{tablename}}_{nameof(UserName)}", nameof(UserName), true)]
[Index($"idx_{{tablename}}_{nameof(Mobile)}",   nameof(Mobile),   true)]
public record TbSysUser : DefaultEntity, IFieldBitSet
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
}