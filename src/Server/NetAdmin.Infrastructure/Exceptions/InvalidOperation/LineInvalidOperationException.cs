namespace NetAdmin.Infrastructure.Exceptions.InvalidOperation;

/// <summary>
///     无效操作异常
/// </summary>
/// <remarks>
///     非正常的业务流程或逻辑
/// </remarks>
#pragma warning disable RCS1194, DesignedForInheritance
public class LineInvalidOperationException : LineException
    #pragma warning restore DesignedForInheritance, RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LineInvalidOperationException" /> class.
    /// </summary>
    public LineInvalidOperationException(string message) //
        : this(message, ErrorCodes.InvalidOperation) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineInvalidOperationException" /> class.
    /// </summary>
    protected LineInvalidOperationException(string message, ErrorCodes errorCode) //
        : base(message, errorCode) { }
}