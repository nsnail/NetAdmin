using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     信息：用户
/// </summary>
public record UserInfo : TbSysUser
{
    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string UserName { get; set; }
}