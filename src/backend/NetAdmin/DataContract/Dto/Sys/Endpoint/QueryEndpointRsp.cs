using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;

namespace NetAdmin.DataContract.Dto.Sys.Endpoint;

/// <summary>
///     信息：端点
/// </summary>
public record QueryEndpointRsp(string Label, string Path) : TbSysEndpoint
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Label { get; set; } = Label;

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Path { get; set; } = Path;
}