using Gurion.FriendlyException;
using NetAdmin.Domain.Dto;

namespace NetAdmin.Host.Filters;

/// <inheritdoc cref="ApiResultHandler{T}" />
[SuppressSniffer]
[UnifyModel(typeof(RestfulInfo<>))]
public sealed class DefaultApiResultHandler : ApiResultHandler<RestfulInfo<object>>, IUnifyResultProvider
{
    /// <inheritdoc />
    public IActionResult OnAuthorizeException(DefaultHttpContext context, ExceptionMetadata metadata)
    {
        LogHelper.Get<DefaultApiResultHandler>().Error(metadata.Exception);
        throw metadata.Exception;
    }
}