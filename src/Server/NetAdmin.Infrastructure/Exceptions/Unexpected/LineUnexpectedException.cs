namespace NetAdmin.Infrastructure.Exceptions.Unexpected;

/// <summary>
///     非预期结果异常
/// </summary>
/// <remarks>
///     运行结果是非预期的，例如事务失败回滚
/// </remarks>
#pragma warning disable RCS1194, DesignedForInheritance
public class LineUnexpectedException : LineException
    #pragma warning restore DesignedForInheritance, RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LineUnexpectedException" /> class.
    /// </summary>
    public LineUnexpectedException(string message) //
        : base(message, ErrorCodes.Unexpected) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineUnexpectedException" /> class.
    /// </summary>
    public LineUnexpectedException() //
        : this(string.Empty) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineUnexpectedException" /> class.
    /// </summary>
    protected LineUnexpectedException(string message, ErrorCodes errorCode) //
        : base(message, errorCode) { }
}