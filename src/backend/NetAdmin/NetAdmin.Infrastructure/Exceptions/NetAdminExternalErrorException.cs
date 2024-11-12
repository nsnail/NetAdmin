namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     外部错误异常
/// </summary>
/// <remarks>
///     外部接口调用未得到预期的结果
/// </remarks>
#pragma warning disable RCS1194
public sealed class NetAdminExternalErrorException(string message, Exception innerException = null)
    #pragma warning restore RCS1194
    : NetAdminException(ErrorCodes.ExternalError, message, innerException) { }