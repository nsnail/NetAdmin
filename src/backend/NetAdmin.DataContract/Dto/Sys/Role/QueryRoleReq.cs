using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：查询角色
/// </summary>
public record QueryRoleReq : TbSysRole
{
    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }
}