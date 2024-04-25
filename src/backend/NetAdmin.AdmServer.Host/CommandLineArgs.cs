using Spectre.Console.Cli;

namespace NetAdmin.AdmServer.Host;

/// <summary>
///     命令行参数
/// </summary>
public sealed class CommandLineArgs : CommandSettings
{
    /// <summary>
    ///     插入种子数据
    /// </summary>
    [CommandOption("-i|--insert-seed-data")]
    public bool InsertSeedData { get; init; }

    /// <summary>
    ///     同步数据库结构
    /// </summary>
    [CommandOption("-s|--sync-structure")]
    public bool SyncStructure { get; init; }
}