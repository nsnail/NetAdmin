using DataType = FreeSql.DataType;

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

    /// <summary>
    ///     种子数据路径（相对）
    /// </summary>
    public string SeedDataRelativePath { get; init; }
}