using Spectre.Console.Cli;

namespace YourSolution.AdmServer.Host;

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
    ///     强制启动计划作业
    /// </summary>
    [CommandOption("-j|--job-force")]
    public bool JobForce { get; init; }

    /// <summary>
    ///     同步数据库结构
    /// </summary>
    [CommandOption("-s|--sync-structure")]
    public bool SyncStructure { get; init; }
}