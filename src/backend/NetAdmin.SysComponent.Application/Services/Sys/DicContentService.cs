using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicContentService" />
public sealed class DicContentService(BasicRepository<Sys_DicContent, long> rpo) //
    : RepositoryService<Sys_DicContent, long, IDicContentService>(rpo), IDicContentService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    public async Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DicCatalog>()
                      .Where(a => a.Id == req.CatalogId)
                      .ForUpdate()
                      .AnyAsync()
                      .ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    public async Task<QueryDicContentRsp> EditAsync(EditDicContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DicCatalog>()
                      .Where(a => a.Id == req.CatalogId)
                      .ForUpdate()
                      .AnyAsync()
                      .ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req, null).ConfigureAwait(false)).FirstOrDefault()
                                                                             ?.Adapt<QueryDicContentRsp>();
        #else
        return await UpdateAsync(req, null).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryDicContentReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryDicContentRsp> GetAsync(QueryDicContentReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDicContentReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryDicContentRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryDicContentRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDicContentRsp>> QueryAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .Take(req.Count)
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDicContentRsp>>();
    }

    private ISelect<Sys_DicContent> QueryInternal(QueryReq<QueryDicContentReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}