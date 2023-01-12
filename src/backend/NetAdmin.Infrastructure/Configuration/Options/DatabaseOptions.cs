using FreeSql;

namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     数据库连接字符串配置
/// </summary>
public record DatabaseOptions : OptionAbstraction
{
    /// <summary>
    ///     链接字符串
    /// </summary>
    public string ConnStr { get; init; }

    /// <summary>
    ///     数据库类型
    /// </summary>
    public DataType DbType { get; init; }
}