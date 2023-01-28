using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mapster;
using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：创建用户
/// </summary>
public record CreateUserReq : TbSysUser, IRegister
{
    /// <inheritdoc cref="TbSysUser.Avatar" />
    [Url]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Avatar { get; init; }

    /// <inheritdoc cref="TbSysUser.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)BitSets.Enabled;
            }

            return ret;
        }
    }

    /// <inheritdoc cref="TbSysUser.DeptId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long DeptId { get; init; }

    /// <inheritdoc cref="TbSysUser.Email" />
    [EmailAddress]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]

    public override string Email { get; init; }

    /// <summary>
    ///     启用
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <inheritdoc cref="TbSysUser.Mobile" />
    [Mobile]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Mobile { get; init; }

    /// <summary>
    ///     密码
    /// </summary>
    [Required]
    [Password]
    public virtual string PasswordText { get; init; }

    /// <summary>
    ///     岗位id列表
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> PositionIds { get; init; }

    /// <summary>
    ///     用户档案
    /// </summary>
    [Required]
    public new virtual CreateUserProfileReq Profile { get; init; }

    /// <summary>
    ///     角色id列表
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(Numbers.BULK_REQ_LIMIT)]
    public IReadOnlyCollection<long> RoleIds { get; init; }

    /// <inheritdoc cref="TbSysUser.UserName" />
    [Required]
    [UserName]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string UserName { get; init; }

    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateUserReq, TbSysUser>()
              .Map(dest => dest.Password, src => src.PasswordText.Pwd().Guid())
              .Map(dest => dest.Token,    src => Guid.NewGuid())
              .Map( //
                  dest => dest.Roles
                , src => src.RoleIds.NullOrEmpty()
                      ? Array.Empty<TbSysRole>()
                      : src.RoleIds.Select(x => new TbSysRole { Id = x }))
              .Map( //
                  dest => dest.Positions
                , src => src.PositionIds.NullOrEmpty()
                      ? Array.Empty<TbSysPosition>()
                      : src.PositionIds.Select(x => new TbSysPosition { Id = x }));
    }
}