using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace NetAdmin.Domain.Events.Sys;

/// <summary>
///     验证码创建事件
/// </summary>
public sealed record VerifyCodeCreatedEvent : EventData<QueryVerifyCodeRsp>
{
    /// <inheritdoc />
    public VerifyCodeCreatedEvent(QueryVerifyCodeRsp payLoad) //
        : base(payLoad) { }
}