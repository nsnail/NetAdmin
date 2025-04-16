namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     状态表示
/// </summary>
[Export]
public enum Indicates
{
    /// <summary>
    ///     信息
    /// </summary>
    Info = 1

   ,

    /// <summary>
    ///     主要
    /// </summary>
    Primary = 2

   ,

    /// <summary>
    ///     警告
    /// </summary>
    Warning = 3

   ,

    /// <summary>
    ///     成功
    /// </summary>
    Success = 4

   ,

    /// <summary>
    ///     危险
    /// </summary>
    Danger = 5

   ,

    /// <summary>
    ///     无
    /// </summary>
    None = 6
}