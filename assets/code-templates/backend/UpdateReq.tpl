namespace {0}.DataContract.Dto.{1}.{2};

/// <summary>
///     请求：更新{3}
/// </summary>
public record Update{2}Req : Create{2}Req {{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version {{ get; init; }}
}}