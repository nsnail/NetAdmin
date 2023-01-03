using System.Net;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using Mapster;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.User;
using NSExt.Extensions;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     登录日志表
/// </summary>
[Table]
public record TbSysLoginLog : ImmutableEntity, IRegister
{
    /// <summary>
    ///     IP地址
    /// </summary>
    [JsonIgnore]
    public virtual string ClientIp { get; set; }

    /// <summary>
    ///     操作耗时（毫秒）
    /// </summary>
    [JsonIgnore]
    public virtual int Duration { get; set; }

    /// <summary>
    ///     是否成功
    /// </summary>
    [JsonIgnore]
    public virtual bool Succeed { get; set; }

    /// <summary>
    ///     用户代理
    /// </summary>
    [JsonIgnore]
    public virtual string UserAgent { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    [JsonIgnore]
    public virtual string UserName { get; set; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TbSysOperationLog, TbSysLoginLog>()
              .Map(dest => dest.Succeed, src => src.ResponseStatusCode == (int)HttpStatusCode.OK)
              .Map( //
                  dest => dest.UserName
                , src => src.RequestParameters.Object<ReqParameter<LoginReq>>(null).Req.UserName);
    }
}