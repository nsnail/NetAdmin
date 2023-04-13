using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDicContentService" />
public sealed class DicContentService : RepositoryService<Sys_DicContent, IDicContentService>, IDicContentService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicContentService" /> class.
    /// </summary>
    public DicContentService(Repository<Sys_DicContent> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除字典内容
    /// </summary>
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建字典内容
    /// </summary>
    /// <exception cref="LineInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    public async Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<Sys_DicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw new LineInvalidOperationException(Ln.Dictionary_directory_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <summary>
    ///     删除字典内容
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDicContentReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryDicContentRsp> GetAsync(QueryDicContentReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询字典内容
    /// </summary>
    public async Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryDicContentRsp>(req.Page, req.PageSize, total
                                                   , list.Adapt<IEnumerable<QueryDicContentRsp>>());
    }

    /// <summary>
    ///     查询字典内容
    /// </summary>
    public async Task<IEnumerable<QueryDicContentRsp>> QueryAsync(QueryReq<QueryDicContentReq> req)
    {
        var ret = await QueryInternal(req).Take(req.Count).ToListAsync();
        return ret.Adapt<IEnumerable<QueryDicContentRsp>>();
    }

    /// <summary>
    ///     更新字典内容
    /// </summary>
    /// <exception cref="LineInvalidOperationException">Dictionary_directory_does_not_exist</exception>
    /// <exception cref="LineUnexpectedException">LineUnexpectedException</exception>
    public async Task<QueryDicContentRsp> UpdateAsync(UpdateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<Sys_DicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw new LineInvalidOperationException(Ln.Dictionary_directory_does_not_exist);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw new LineUnexpectedException();
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDicContentRsp>();
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