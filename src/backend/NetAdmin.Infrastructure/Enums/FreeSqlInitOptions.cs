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
    [ResourceDescription<Ln>(nameof(Ln.同步数据库结构))]
    SyncStructure = 1

   ,

    /// <summary>
    ///     插入种子数据
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.插入种子数据))]
    InsertSeedData = 1 << 1

   ,

    /// <summary>
    ///     比较数据库结构
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.比较数据库结构))]
    CompareStructure = 1 << 2
}