using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.VerifyCode;

/// <summary>
///     请求：更新验证码
/// </summary>
public sealed record UpdateVerifyCodeReq : CreateVerifyCodeReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}