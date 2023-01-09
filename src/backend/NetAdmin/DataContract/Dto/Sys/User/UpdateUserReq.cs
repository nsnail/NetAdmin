using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：更新用户
/// </summary>
public record UpdateUserReq : CreateUserReq
{
    /// <inheritdoc cref="TbSysUser.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)Enums.SysUserBits.Enabled;
            }

            return ret;
        }
    }

    /// <summary>
    ///     启用
    /// </summary>
    [Required]
    public bool Enabled { get; set; }

    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    public override string PasswordText => nameof(PasswordText);

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; set; }
}