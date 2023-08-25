using NetAdmin.Domain.Attributes.DataValidation;

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
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.请求对象))]
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IEnumerable<T> Items { get; init; }
}