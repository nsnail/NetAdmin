using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：修改角色
/// </summary>
public record UpdateRoleReq : CreateRoleReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override long Version { get; init; }
}