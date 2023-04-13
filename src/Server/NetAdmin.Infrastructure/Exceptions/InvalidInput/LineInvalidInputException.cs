namespace NetAdmin.Infrastructure.Exceptions.InvalidInput;

/// <summary>
///     无效输入异常
/// </summary>
/// <remarks>
///     参数格式错误、内容校验错误等
/// </remarks>
#pragma warning disable RCS1194
public sealed class LineInvalidInputException : LineException
    #pragma warning restore RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LineInvalidInputException" /> class.
    /// </summary>
    public LineInvalidInputException(string message) //
        : base(message, ErrorCodes.InvalidInput) { }
}