namespace NetAdmin.Domain.Dto.Sys.Tool;

/// <summary>
///     请求：执行SQL
/// </summary>
public record ExecuteSqlReq : DataAbstraction
{
    /// <summary>
    ///     SQL 语句
    /// </summary>
    public string Sql { get; init; }

    /// <summary>
    ///     超时时间（秒）
    /// </summary>
    public int TimeoutSecs { get; init; }
}