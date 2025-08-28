namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     无效输入异常
/// </summary>
/// <remarks>
///     参数格式错误、内容校验错误等
/// </remarks>
#pragma warning disable DesignedForInheritance, RCS1194
public class NetAdminInvalidInputException(string message = null, Exception innerException = null)
    #pragma warning restore RCS1194, DesignedForInheritance
    : NetAdminException(ErrorCodes.InvalidInput, message, innerException)
{
}