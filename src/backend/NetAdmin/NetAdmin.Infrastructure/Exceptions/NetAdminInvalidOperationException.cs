#pragma warning disable RCS1194, DesignedForInheritance
namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     无效操作异常
/// </summary>
/// <remarks>
///     非正常的业务流程或逻辑
/// </remarks>
public class NetAdminInvalidOperationException(string message, Exception innerException = null)
    : NetAdminException(ErrorCodes.InvalidOperation, message, innerException);