namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     无效操作异常
/// </summary>
/// <remarks>
///     非正常的业务流程或逻辑
/// </remarks>
#pragma warning disable DesignedForInheritance, RCS1194
public class NetAdminInvalidOperationException(string message, Exception innerException = null)
    #pragma warning restore RCS1194, DesignedForInheritance
    : NetAdminException(ErrorCodes.InvalidOperation, message, innerException) { }