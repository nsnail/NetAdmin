using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;

namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     用户编号验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class UserIdAttribute : ValidationAttribute
{
    /// <inheritdoc />
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = App.GetService(App.EffectiveTypes.Single(
                                         x => x.FullName == "NetAdmin.SysComponent.Cache.Sys.Dependency.IUserCache"));

        var req = new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = (long)value! } };

        var method = service.GetType().GetMethod("ExistAsync");
        var exist  = ((Task<bool>)method!.Invoke(service, [req]))!.ConfigureAwait(false).GetAwaiter().GetResult();
        return !exist ? new ValidationResult(Ln.用户编号不存在) : ValidationResult.Success;
    }
}