using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     登录日志表
/// </summary>
[Table]
public record TbSysLoginLog : ImmutableEntity
{
    /// <summary>
    ///     IP地址
    /// </summary>
    [JsonIgnore]
    public virtual string ClientIp { get; init; }

    /// <summary>
    ///     操作耗时（毫秒）
    /// </summary>
    [JsonIgnore]
    public virtual int Duration { get; init; }

    /// <summary>
    ///     是否成功
    /// </summary>
    [JsonIgnore]
    public virtual bool Succeed { get; init; }

    /// <summary>
    ///     用户代理
    /// </summary>
    [JsonIgnore]
    public virtual string UserAgent { get; init; }

    /// <summary>
    ///     用户名
    /// </summary>
    [JsonIgnore]
    public virtual string UserName { get; init; }
}