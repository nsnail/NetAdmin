namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     角色数据范围
/// </summary>
[Export]
public enum DataScopes
{
    /// <summary>
    ///     全部
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.全部))]
    All = 1

   ,

    /// <summary>
    ///     本部门和下级部门
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.本部门和下级部门))]
    DeptWithChild = 2

   ,

    /// <summary>
    ///     本部门
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.本部门数据))]
    Dept = 3

   ,

    /// <summary>
    ///     本人数据
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.本人数据))]
    Self = 4

   ,

    /// <summary>
    ///     指定部门
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.指定部门))]
    SpecificDept = 5
}