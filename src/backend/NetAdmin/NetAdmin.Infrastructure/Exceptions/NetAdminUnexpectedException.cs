namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     非预期结果异常
/// </summary>
/// <remarks>
///     运行结果是非预期的，例如事务失败回滚
/// </remarks>
#pragma warning disable RCS1194
public sealed class NetAdminUnexpectedException(string message, Exception innerException = null)
    #pragma warning restore RCS1194
    : NetAdminException(ErrorCodes.Unexpected, message, innerException);