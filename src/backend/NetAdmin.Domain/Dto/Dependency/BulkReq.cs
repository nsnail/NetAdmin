namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     批量请求
/// </summary>
public sealed record BulkReq<T> : DataAbstraction
    where T : DataAbstraction, new()
{
    /// <summary>
    ///     请求对象
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.请求对象不能为空))]
    [MinLength(1)]
    [MaxLength(Numbers.MAX_LIMIT_BULK_REQ)]
    public IEnumerable<T> Items { get; init; }
}