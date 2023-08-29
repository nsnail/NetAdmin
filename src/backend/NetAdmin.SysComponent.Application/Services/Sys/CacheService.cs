using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using StackExchange.Redis;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICacheService" />
public sealed class CacheService : ServiceBase<ICacheService>, ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheService" /> class.
    /// </summary>
    public CacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    /// <inheritdoc />
    public Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        var database = _connectionMultiplexer.GetDatabase();
        return Task.FromResult(
            new CacheStatisticsRsp((string)database.Execute("info")) { DbSize = (long)database.Execute("dbSize") });
    }

    /// <inheritdoc />
    public PagedQueryRsp<GetAllEntriesRsp> GetAllEntries(PagedQueryReq<GetAllEntriesReq> req)
    {
        var database = _connectionMultiplexer.GetDatabase((int?)req.Filter?.DbIndex ?? 0);
        var redisResults
            = (RedisResult[])database.Execute("scan", (req.Page - 1) * req.PageSize, "count", req.PageSize);

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