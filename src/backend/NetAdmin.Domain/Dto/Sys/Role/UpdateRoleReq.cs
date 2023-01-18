using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：修改角色
/// </summary>
public record UpdateRoleReq : CreateRoleReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Required]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [Required]
    public override long Version { get; init; }
}