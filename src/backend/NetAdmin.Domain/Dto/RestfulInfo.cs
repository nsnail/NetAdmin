namespace NetAdmin.Domain.Dto;

/// <summary>
///     信息：RESTful 风格结果集
/// </summary>
public record RestfulInfo<T> : DataAbstraction
{
    /// <summary>
    ///     代码
    /// </summary>
    public Enums.RspCodes Code { get; init; }

    /// <summary>
    ///     数据
    /// </summary>
    public T Data { get; init; }

    /// <summary>
    ///     消息
    /// </summary>
    public object Msg { get; init; }
}