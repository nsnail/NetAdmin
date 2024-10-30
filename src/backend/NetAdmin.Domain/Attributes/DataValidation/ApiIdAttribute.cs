using NetAdmin.Domain.Dto.Sys.Api;

namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     接口编码验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class ApiIdAttribute : ValidationAttribute
{
    /// <inheritdoc />
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var service = App.GetService(App.EffectiveTypes.Single(x => x.FullName == "NetAdmin.SysComponent.Cache.Sys.Dependency.IApiCache"));

        var req = new QueryReq<QueryApiReq> { Filter = new QueryApiReq { Id = value as string } };

        var method = service.GetType().GetMethod("ExistAsync");
        var exist  = ((Task<bool>)method!.Invoke(service, [req]))!.ConfigureAwait(false).GetAwaiter().GetResult();
        return !exist ? new ValidationResult(Ln.接口编码不存在) : ValidationResult.Success;
    }
}