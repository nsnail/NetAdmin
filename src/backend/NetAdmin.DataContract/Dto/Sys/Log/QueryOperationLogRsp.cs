using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.DataContract.DbMaps;

namespace NetAdmin.DataContract.Dto.Sys.Log;

/// <summary>
///     响应：查询操作日志
/// </summary>
public record QueryOperationLogRsp : TbSysOperationLog, IRegister
{
    /// <summary>
    ///     操作系统
    /// </summary>
    public string Os => new UserAgentParser(UserAgent).Platform;

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ApiId { get; init; }

    /// <summary>
    ///     接口描述
    /// </summary>
    public string ApiSummary { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ClientIp { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string CreatedUserName { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override uint Duration { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Environment { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Exception { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Method { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ReferUrl { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string RequestBody { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string RequestContentType { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string RequestHeaders { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string RequestUrl { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ResponseBody { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ResponseContentType { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ResponseHeaders { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string ServerIp { get; init; }

    /// <inheritdoc />
    public override int StatusCode { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserAgent { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<TbSysOperationLog, QueryOperationLogRsp>().Map(dest => dest.ApiSummary, src => src.Api.Summary);
    }
}