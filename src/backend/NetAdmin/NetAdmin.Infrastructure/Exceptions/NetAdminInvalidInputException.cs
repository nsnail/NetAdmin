#pragma warning disable RCS1194, DesignedForInheritance
namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     无效输入异常
/// </summary>
/// <remarks>
///     参数格式错误、内容校验错误等
/// </remarks>
public class NetAdminInvalidInputException(string message = null, Exception innerException = null)
    : NetAdminException(ErrorCodes.InvalidInput, message, innerException);