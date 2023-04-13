using NetAdmin.Domain.Dto;

namespace NetAdmin.Host.Filters;

/// <summary>
///     Api结果格式化处理器
/// </summary>
/// <remarks>
///     约定：
///     1、业务异常需要设置HttpStatusCode与成功请求区分，不要统一返回200（ 为了方便前端调试：在浏览器F12中快速观察失败请求【红色高亮】）
///     2、不得占用常见HttpStatusCode，例如4xx、5xx，以免与传输层错误混淆，干扰运维。
///     实现：
///     1、本系统代码覆盖范围内占用4个HttpStatusCode：200（表示业务成功）、401（身份未确认）、403（权限不足）、900（其他所有业务异常）
///     2、当HttpStatusCode为900时，通过子码（JsonBody里面的Code区分具体异常），同时将子码写入RspHeader中，方便日志系统快速筛选归类。
///     3、子码定义，见枚举 <see cref="ErrorCodes" />
/// </remarks>
[SuppressSniffer]
[UnifyModel(typeof(RestfulInfo<>))]
public sealed class DefaultApiResultHandler : ApiResultHandler<RestfulInfo<object>>, IUnifyResultProvider { }