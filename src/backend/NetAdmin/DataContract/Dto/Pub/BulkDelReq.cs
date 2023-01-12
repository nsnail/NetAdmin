using System.ComponentModel.DataAnnotations;

namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     请求：通过id批量删除
/// </summary>
public record BulkDelReq : DataAbstraction
{
    /// <summary>
    ///     要删除的id列表
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public IReadOnlyCollection<long> Ids { get; init; }
}