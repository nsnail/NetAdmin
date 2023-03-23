namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     请求：更新字典内容
/// </summary>
public record UpdateDicContentReq : CreateDicContentReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}