namespace NetAdmin.Domain.Dto.Sys.CodeTemplate;

/// <summary>
///     请求：创建代码模板
/// </summary>
public record CreateCodeTemplateReq : Sys_CodeTemplate
{
    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    public override bool Enabled { get; init; } = true;

    /// <inheritdoc cref="Sys_CodeTemplate.Gender" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Genders? Gender { get; init; }

    /// <inheritdoc cref="Sys_CodeTemplate.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_CodeTemplate.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; }

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }
}