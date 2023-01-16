using NetAdmin.DataContract.Dto.Sys.RequestLog;

namespace NetAdmin.Host.Events.Sources;

/// <summary>
///     操作事件
/// </summary>
public class OperationEvent : IEventSourceGeneric<CreateRequestLogReq>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OperationEvent" /> class.
    /// </summary>
    public OperationEvent(CreateRequestLogReq data)
    {
        Data = data;
    }

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; }

    /// <inheritdoc />
    public CreateRequestLogReq Data { get; }

    /// <inheritdoc />
    public string EventId => nameof(OperationEvent);

    /// <inheritdoc />
    public object Payload { get; }
}