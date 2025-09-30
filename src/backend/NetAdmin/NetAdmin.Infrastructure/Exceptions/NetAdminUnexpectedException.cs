#pragma warning disable RCS1194
namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     非预期结果异常
/// </summary>
/// <remarks>
///     运行结果是非预期的，例如事务失败回滚
/// </remarks>
public sealed class NetAdminUnexpectedException(string message, Exception innerException = null)
    : NetAdminException(ErrorCodes.Unexpected, message, innerException);