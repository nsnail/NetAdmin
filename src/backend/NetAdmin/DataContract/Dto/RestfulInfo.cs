using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto;

/// <summary>
///     信息：RESTful 风格结果集
/// </summary>
public record RestfulInfo<T> : DataAbstraction
{
    /// <summary>
    ///     代码
    /// </summary>
    public Enums.ErrorCodes Code { get; set; }

    /// <summary>
    ///     数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    ///     消息
    /// </summary>
    public object Msg { get; set; }
}