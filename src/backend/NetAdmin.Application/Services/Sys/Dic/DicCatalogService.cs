using FreeSql;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency.Dic;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;

namespace NetAdmin.Application.Services.Sys.Dic;

/// <inheritdoc cref="IDicCatalogService" />
public class DicCatalogService : RepositoryService<TbSysDicCatalog, IDicCatalogService>, IDicCatalogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicCatalogService" /> class.
    /// </summary>
    public DicCatalogService(Repository<TbSysDicCatalog> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除字典目录
    /// </summary>
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建字典目录
    /// </summary>
    public async Task<QueryDicCatalogRsp> Create(CreateDicCatalogReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.The_parent_node_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryDicCatalogRsp>();
    }

    /// <summary>
    ///     删除字典目录
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteCascadeByDatabaseAsync(a => a.Id == req.Id);
        return ret.Count;
    }

    /// <summary>
    ///     分页查询字典目录
    /// </summary>
    public async Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQuery(PagedQueryReq<QueryDicCatalogReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryDicCatalogRsp>(req.Page, req.PageSize, total
                                                   , list.Select(x => x.Adapt<QueryDicCatalogRsp>()));
    }

    /// <summary>
    ///     查询字典目录
    /// </summary>
    public async Task<IEnumerable<QueryDicCatalogRsp>> Query(QueryReq<QueryDicCatalogReq> req)
    {
        var ret = await QueryInternal(req).ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryDicCatalogRsp>>();
    }

    /// <summary>
    ///     更新字典目录
    /// </summary>
    public async Task<QueryDicCatalogRsp> Update(UpdateDicCatalogReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.The_parent_node_does_not_exist);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDicCatalogRsp>();
    }

    private ISelect<TbSysDicCatalog> QueryInternal(QueryReq<QueryDicCatalogReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }
}