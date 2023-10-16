namespace NetAdmin.Infrastructure.Exceptions.InvalidInput;

/// <summary>
///     无效输入异常
/// </summary>
/// <remarks>
///     参数格式错误、内容校验错误等
/// </remarks>
#pragma warning disable RCS1194
public sealed class NetAdminInvalidInputException
    (string message = null, Exception innerException = null) : NetAdminException(
        ErrorCodes.InvalidInput, message, innerException)
    #pragma warning restore RCS1194
{ }