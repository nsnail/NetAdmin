namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     请求：创建字典内容
/// </summary>
public record CreateDicContentReq : Sys_DicContent
{
    /// <inheritdoc cref="Sys_DicContent.CatalogId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.字典目录编号不能为空))]
    public override long CatalogId { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; } = true;

    /// <inheritdoc cref="Sys_DicContent.Key" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.键名称不能为空))]
    public override string Key { get; init; }

    /// <inheritdoc cref="Sys_DicContent.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; set; }

    /// <inheritdoc cref="Sys_DicContent.Value" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.键值不能为空))]
    public override string Value { get; init; }
}