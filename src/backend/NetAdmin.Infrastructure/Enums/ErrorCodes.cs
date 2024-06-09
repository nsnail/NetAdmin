namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     错误码
/// </summary>
[Export]
public enum ErrorCodes
{
    /// <summary>
    ///     成功
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.成功))]
    Succeed = 0

   ,

    /// <summary>
    ///     未处理异常
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.未处理异常))]
    Unhandled = 9000

   ,

    /// <summary>
    ///     结果非预期
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.结果非预期))]
    Unexpected = 9100

   ,

    /// <summary>
    ///     无效输入
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.无效输入))]
    InvalidInput = 9200

   ,

    /// <summary>
    ///     无效操作
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.无效操作))]
    InvalidOperation = 9300

   ,

    /// <summary>
    ///     外部错误
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.外部错误))]
    ExternalError = 9400
}