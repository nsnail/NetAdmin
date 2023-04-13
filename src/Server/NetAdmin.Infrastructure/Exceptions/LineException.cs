namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     Line异常基类
/// </summary>
#pragma warning disable RCS1194
public abstract class LineException : Exception
    #pragma warning restore RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LineException" /> class.
    /// </summary>
    protected LineException() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineException" /> class.
    /// </summary>
    protected LineException(string message, ErrorCodes code) //
        : base(message)
    {
        Code = code;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="LineException" /> class.
    /// </summary>
    protected LineException(string message, Exception innerException) //
        : base(message, innerException) { }

    /// <summary>
    ///     错误码
    /// </summary>
    public ErrorCodes Code { get; set; }
}