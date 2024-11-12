namespace NetAdmin.Domain.Enums;

/// <summary>
///     动态查询条件逻辑运算符
/// </summary>
[Export]
public enum DynamicFilterLogics
{
    /// <summary>
    ///     并且
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.并且))]
    And = 0

   ,

    /// <summary>
    ///     或者
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.或者))]
    Or = 1
}