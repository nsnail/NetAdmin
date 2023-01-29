using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.Extensions.Caching.Memory;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;

namespace NetAdmin.Host.Caches.Sys.Implements;

/// <inheritdoc cref="ISmsCache" />
public class SmsCache : CacheBase<ISmsService>, IScoped, ISmsCache
{
    private const int _CACHE_EXPIRES_SMSCODE = 300;
    private const int _SEND_LIMIT_SMSCODE    = 60;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsCache" /> class.
    /// </summary>
    public SmsCache(IMemoryCache cache, ISmsService service) //
        : base(cache, service) { }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    public async Task<SendSmsCodeRsp> SendSmsCode(SendSmsCodeReq req)
    {
        // 如果缓存（手机号做key）存在，且创建时间小于1分钟，不得再次发送
        var id = $"{nameof(SendSmsCode)}_{req.Mobile}";

        Cache.TryGetValue(id, out var val);
        if (val is SendSmsCodeRsp sentCode) {
            var timeInterval = (DateTime.Now - sentCode.CreateTime).TotalSeconds;
            if (timeInterval < _SEND_LIMIT_SMSCODE) {
                throw Oops.Oh(Enums.RspCodes.InvalidOperation);
            }
        }

        var ret = await Service.SendSmsCode(req);

        // 写入缓存，用于校验
        Cache.Set(id, ret, TimeSpan.FromSeconds(_CACHE_EXPIRES_SMSCODE));
        return ret;
    }

    /// <inheritdoc />
    public async Task<bool> VerifySmsCode(VerifySmsCodeReq req)
    {
        return await Service.VerifySmsCode(req);
    }
}