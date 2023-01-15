using System.Globalization;
using Furion.EventBus;

namespace NetAdmin.Host.Events.Sources;

/// <summary>
///     Sql命令事件
/// </summary>
public class SqlCommandEvent : IEventSource
{
    /// <summary>
    ///     标识符缩写
    /// </summary>
    public string Id => Identifier.ToString()[..8].ToUpper(CultureInfo.InvariantCulture);

    /// <inheritdoc />
    public CancellationToken CancellationToken { get; init; }

    /// <inheritdoc />
    public DateTime CreatedTime { get; protected init; }

    /// <inheritdoc />
    public string EventId { get; protected init; }

    /// <summary>
    ///     标识符，可将 CommandBefore 与 CommandAfter 进行匹配
    /// </summary>
    public Guid Identifier { get; protected init; }

    /// <inheritdoc />
    public object Payload { get; init; }

    /// <summary>
    ///     关联的Sql语句
    /// </summary>
    public string Sql { get; protected init; }
}