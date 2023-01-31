namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     请求：通过id删除
/// </summary>
public record DelReq : DataAbstraction
{
    /// <summary>
    ///     要删除的id
    /// </summary>
    public long Id { get; init; }
}