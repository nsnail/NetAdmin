using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.Dto.Sys.UserProfile;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：更新用户
/// </summary>
public record UpdateUserReq : CreateUserReq
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <summary>
    ///     此属性无意义，只是为了通过数据校验
    /// </summary>
    [JsonIgnore]
    public override string PasswordText => "MOCK-1234";

    /// <summary>
    ///     用户档案
    /// </summary>
    [Required]
    public new virtual UpdateUserProfileReq Profile { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}