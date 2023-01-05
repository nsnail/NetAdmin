using System.ComponentModel.DataAnnotations;
using NetAdmin.DataContract;

namespace NetAdmin.Api.Pub;

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
    public List<long> Ids { get; set; }
}