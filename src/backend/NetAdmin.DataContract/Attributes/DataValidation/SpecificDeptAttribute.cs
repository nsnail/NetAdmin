using System.ComponentModel.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Sys.Role;

namespace NetAdmin.DataContract.Attributes.DataValidation;

/// <summary>
///     数据范围为特定部门的验证器
/// </summary>
public class SpecificDeptAttribute : ValidationAttribute
{
    /// <inheritdoc />
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return validationContext.ObjectInstance is not CreateRoleReq { DataScope: TbSysRole.DataScopes.SpecificDept }
            ?
            ValidationResult.Success
            : (value as IEnumerable<long>)?.Any() ?? false
                ? ValidationResult.Success
                : new ValidationResult(Ln.No_department_specified);
    }
}