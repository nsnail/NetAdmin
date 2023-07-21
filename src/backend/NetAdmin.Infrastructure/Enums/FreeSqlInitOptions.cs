namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     初始化选项
/// </summary>
[Flags]
public enum FreeSqlInitOptions
{
    /// <summary>
    ///     无
    /// </summary>
    None = 0

   ,

    /// <summary>
    ///     同步数据库结构
    /// </summary>
    SyncStructure = 1

   ,

    /// <summary>
    ///     插入种子数据
    /// </summary>
    InsertSeedData = 1 << 1

   ,

    /// <summary>
    ///     比较数据库结构
    /// </summary>
    CompareStructure = 1 << 2
}