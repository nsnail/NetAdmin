namespace NetAdmin.Domain.Dto.Sys.DepositOrder;

/// <summary>
///     请求：到账确认
/// </summary>
public record ReceivedConfirmationReq : DataAbstraction
{
    /// <summary>
    ///     读取前n条记录
    /// </summary>
    public int ReadRecordCount { get; init; }
}