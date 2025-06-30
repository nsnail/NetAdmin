namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：检查邀请码是否正确
/// </summary>
public sealed record CheckInviterAvailableReq : DataAbstraction
{
    /// <summary>
    ///     邀请码
    /// </summary>
    [Required]
    public string Code { get; init; }
}