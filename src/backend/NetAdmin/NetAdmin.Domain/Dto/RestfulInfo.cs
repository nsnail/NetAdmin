namespace NetAdmin.Domain.Dto;

/// <summary>
///     信息：RESTful 风格结果集
/// </summary>
public record RestfulInfo<T> : DataAbstraction
{
    /// <summary>
    ///     代码
    /// </summary>
    /// <example>succeed</example>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ErrorCodes Code { get; init; }

    /// <summary>
    ///     数据
    /// </summary>
    public T Data { get; init; }

    /// <summary>
    ///     字符串："消息内容"，或数组：[{"参数名1":"消息内容1"},{"参数名2":"消息内容2"}]
    /// </summary>
    /// <example>请求成功</example>
    public object Msg { get; init; }
}