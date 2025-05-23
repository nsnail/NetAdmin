namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     计划作业状态
/// </summary>
[Export]
public enum JobStatues
{
    /// <summary>
    ///     空闲
    /// </summary>
    [EnumDecoration(nameof(Indicates.Success))]
    [ResourceDescription<Ln>(nameof(Ln.空闲))]
    Idle = 1

   ,

    /// <summary>
    ///     运行
    /// </summary>
    [EnumDecoration(nameof(Indicates.Warning), true)]
    [ResourceDescription<Ln>(nameof(Ln.运行))]
    Running = 2
}