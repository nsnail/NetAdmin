namespace NetAdmin.Infrastructure.Exceptions.InvalidOperation;

/// <summary>
///     任务状态变更异常
/// </summary>
#pragma warning disable RCS1194
public sealed class LineTaskStatusChangeException : LineInvalidOperationException
    #pragma warning restore RCS1194
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="LineTaskStatusChangeException" /> class.
    /// </summary>
    public LineTaskStatusChangeException(string message) //
        : base(message, ErrorCodes.TaskStatusChangeIllegal) { }
}