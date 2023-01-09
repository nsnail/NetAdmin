using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.Aop.Attributes.DataValidation;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public record CreateUserReq : TbSysUser, IRegister
{
    /// <inheritdoc cref="TbSysUser.Avatar" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Url]
    public override string Avatar { get; set; }

    /// <inheritdoc cref="TbSysUser.DeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DeptId { get; set; }

    /// <inheritdoc cref="TbSysUser.Mobile" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Mobile]
    public override string Mobile { get; set; }

    /// <summary>
    ///     密码
    /// </summary>
    [Required]
    [Password]
    public virtual string PasswordText { get; set; }

    /// <summary>
    ///     角色id列表
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public List<long> RoleIds { get; set; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    [UserName]
    public override string UserName { get; set; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateUserReq, TbSysUser>()
              .Map(dest => dest.Password, src => src.PasswordText.Pwd().Guid())
              .Map(dest => dest.Token,    src => Guid.NewGuid())
              .Map(dest => dest.BitSet,   src => Enums.SysUserBits.Enabled);
    }
}