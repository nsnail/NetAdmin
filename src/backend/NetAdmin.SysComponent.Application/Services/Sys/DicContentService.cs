using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicContentService" />
public sealed class DicContentService(DefaultRepository<Sys_DicContent> rpo) //
    : RepositoryService<Sys_DicContent, IDicContentService>(rpo), IDicContentService
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
    public Task<bool> ExistAsync(QueryReq<QueryDicContentReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
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
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync().ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryDicContentRsp>>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    /// <exception cref="NetAdminUnexpectedException">NetAdminUnexpectedException</exception>
    public async Task<QueryDicContentRsp> UpdateAsync(UpdateDicContentReq req)
    {
        req.ThrowIfInvalid();
        if (!await Rpo.Orm.Select<Sys_DicCatalog>()
                      .Where(a => a.Id == req.CatalogId)
                      .ForUpdate()
                      .AnyAsync()
                      .ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = await Rpo.Where(a => a.Id == req.Id).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    protected override Task<Sys_DicContent> UpdateForSqliteAsync(Sys_DicContent req)
    {
        throw new NotImplementedException();
    }

    private ISelect<Sys_DicContent> QueryInternal(QueryReq<QueryDicContentReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }
}