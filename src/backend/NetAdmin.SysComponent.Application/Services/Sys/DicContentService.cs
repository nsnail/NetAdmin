using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicContentService" />
public sealed class DicContentService
    (DefaultRepository<Sys_DicContent> rpo) : RepositoryService<Sys_DicContent, IDicContentService>(rpo), IDicContentService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    public async Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<Sys_DicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryDicContentReq> req)
    {
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public async Task<QueryDicContentRsp> GetAsync(QueryDicContentReq req)
    {
        var ret = await QueryInternal(new QueryReq<QueryDicContentReq> { Filter = req }).ToOneAsync();
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryDicContentRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryDicContentRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDicContentRsp>> QueryAsync(QueryReq<QueryDicContentReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryDicContentRsp>>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    /// <exception cref="NetAdminUnexpectedException">NetAdminUnexpectedException</exception>
    public async Task<QueryDicContentRsp> UpdateAsync(UpdateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<Sys_DicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw new NetAdminInvalidOperationException(Ln.字典目录不存在);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
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