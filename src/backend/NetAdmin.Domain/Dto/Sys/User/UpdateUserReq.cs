using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：更新用户
/// </summary>
public record UpdateUserReq : CreateUserReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <summary>
    ///     此属性无意义
    /// </summary>
    [JsonIgnore]
    public override string PasswordText => nameof(PasswordText);

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}