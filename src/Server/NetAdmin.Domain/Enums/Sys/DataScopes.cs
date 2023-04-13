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
    [ResourceDescription<Ln>(nameof(Ln.All))]
    All = 1

   ,

    /// <summary>
    ///     本部门和下级部门
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.This_department_and_subordinate_departments))]
    DeptWithChild = 2

   ,

    /// <summary>
    ///     本部门
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Department_data))]
    Dept = 3

   ,

    /// <summary>
    ///     本人数据
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Personal_data))]
    Self = 4

   ,

    /// <summary>
    ///     指定部门
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Designated_department))]
    SpecificDept = 5
}