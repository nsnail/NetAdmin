using System.ComponentModel.DataAnnotations;
using NetAdmin.DataContract.Dto.Sys.Role;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Lang;

namespace NetAdmin.DataContract.Attributes.DataValidation;

/// <summary>
///     数据范围为特定部门的验证器
/// </summary>
public class SpecificDeptAttribute : ValidationAttribute
{
    /// <inheritdoc />
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        return validationContext.ObjectInstance is not CreateRoleReq { DataScope: Enums.DataScopes.SpecificDept }
            ?
            ValidationResult.Success
            : (value as IEnumerable<long>)?.Any() ?? false
                ? ValidationResult.Success
                : new ValidationResult(Str.No_department_specified);
    }
}