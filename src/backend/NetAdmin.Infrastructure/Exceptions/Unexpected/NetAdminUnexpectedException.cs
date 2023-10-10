namespace NetAdmin.Infrastructure.Exceptions.Unexpected;

/// <summary>
///     非预期结果异常
/// </summary>
/// <remarks>
///     运行结果是非预期的，例如事务失败回滚
/// </remarks>
#pragma warning disable RCS1194, DesignedForInheritance
public class NetAdminUnexpectedException : NetAdminException
    #pragma warning restore DesignedForInheritance, RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NetAdminUnexpectedException" /> class.
    /// </summary>
    public NetAdminUnexpectedException(string message) //
        : this(ErrorCodes.Unexpected, message) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NetAdminUnexpectedException" /> class.
    /// </summary>
    public NetAdminUnexpectedException() //
        : this(string.Empty) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NetAdminUnexpectedException" /> class.
    /// </summary>
    protected NetAdminUnexpectedException(ErrorCodes errorCode, string message) //
        : base(errorCode, message) { }
}