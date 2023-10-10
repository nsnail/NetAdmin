using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：检查用户名是否可用
/// </summary>
public sealed record CheckUserNameAvailableReq : Sys_User
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_User.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.用户名不能为空))]
    [UserName]
    public override string UserName { get; init; }
}