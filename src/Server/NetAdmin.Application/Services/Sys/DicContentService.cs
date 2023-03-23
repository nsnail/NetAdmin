using Furion.FriendlyException;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IDicContentService" />
public class DicContentService : RepositoryService<TbSysDicContent, IDicContentService>, IDicContentService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicContentService" /> class.
    /// </summary>
    public DicContentService(Repository<TbSysDicContent> rpo) //
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
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<TbSysDicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Dictionary_directory_does_not_exist);
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
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<QueryDicContentRsp> UpdateAsync(UpdateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<TbSysDicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Dictionary_directory_does_not_exist);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDicContentRsp>();
    }

    private ISelect<TbSysDicContent> QueryInternal(QueryReq<QueryDicContentReq> req)
    {
        return Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                  .WhereDynamic(req.Filter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                  .OrderByDescending(a => a.Id);
    }
}