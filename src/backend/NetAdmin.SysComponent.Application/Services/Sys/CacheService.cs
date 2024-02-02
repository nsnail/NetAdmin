using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using StackExchange.Redis;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICacheService" />
public sealed class CacheService(IConnectionMultiplexer connectionMultiplexer) //
    : ServiceBase<ICacheService>, ICacheService
{
    /// <inheritdoc />
    public async Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        var database = connectionMultiplexer.GetDatabase();

        return new CacheStatisticsRsp((string)await database.ExecuteAsync("info").ConfigureAwait(false)) {
                   DbSize = (long)await database.ExecuteAsync("dbSize").ConfigureAwait(false)
               };
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<GetAllEntriesRsp>> GetAllEntriesAsync(PagedQueryReq<GetAllEntriesReq> req)
    {
        req.ThrowIfInvalid();
        var database = connectionMultiplexer.GetDatabase((int?)req.Filter?.DbIndex ?? 0);
        var redisResults = (RedisResult[])await database
                                                .ExecuteAsync("scan", (req.Page - 1) * req.PageSize, "count"
                                                    ,                 req.PageSize)
                                                .ConfigureAwait(false);

        var list = ((string[])redisResults![1])!.Where(x => database.KeyType(x) == RedisType.Hash)
                                                .Select(x => database.HashGetAll(x)
                                                                     .Append(new HashEntry("key", x))
                                                                     .ToArray()
                                                                     .ToStringDictionary())
                                                .ToList()
                                                .ConvertAll(x => x.Adapt<GetAllEntriesRsp>());

        return new PagedQueryRsp<GetAllEntriesRsp>(req.Page, req.PageSize, 10000, list);
    }
}