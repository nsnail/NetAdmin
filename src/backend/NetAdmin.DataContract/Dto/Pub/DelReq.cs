using System.ComponentModel.DataAnnotations;

namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     请求：通过id删除
/// </summary>
public record DelReq : DataAbstraction
{
    /// <summary>
    ///     要删除的id
    /// </summary>
    [Required]
    public long Id { get; init; }
}