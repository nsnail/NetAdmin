using NetAdmin.Domain.Dto;

namespace NetAdmin.Host.Filters;

/// <inheritdoc cref="NetAdmin.Host.Filters.ApiResultHandler{T}" />
[SuppressSniffer]
[UnifyModel(typeof(RestfulInfo<>))]
public sealed class DefaultApiResultHandler : ApiResultHandler<RestfulInfo<object>>, IUnifyResultProvider;