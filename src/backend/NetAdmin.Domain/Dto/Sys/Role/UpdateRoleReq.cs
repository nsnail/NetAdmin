using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：修改角色
/// </summary>
public sealed record UpdateRoleReq : CreateRoleReq
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.唯一编码))]
    public override long Id { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.数据版本))]
    public override long Version { get; init; }
}