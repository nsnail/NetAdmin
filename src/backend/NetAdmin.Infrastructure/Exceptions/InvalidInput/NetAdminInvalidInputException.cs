namespace NetAdmin.Infrastructure.Exceptions.InvalidInput;

/// <summary>
///     无效输入异常
/// </summary>
/// <remarks>
///     参数格式错误、内容校验错误等
/// </remarks>
#pragma warning disable RCS1194
public sealed class NetAdminInvalidInputException : NetAdminException
    #pragma warning restore RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NetAdminInvalidInputException" /> class.
    /// </summary>
    public NetAdminInvalidInputException(string message = null, Exception innerException = null) //
        : base(ErrorCodes.InvalidInput, message, innerException) { }
}