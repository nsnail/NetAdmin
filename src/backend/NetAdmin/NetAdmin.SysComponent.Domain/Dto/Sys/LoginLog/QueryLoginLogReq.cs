namespace NetAdmin.SysComponent.Domain.Dto.Sys.LoginLog;

/// <summary>
///     请求：查询登录日志
/// </summary>
public sealed record QueryLoginLogReq : Sys_LoginLog
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}