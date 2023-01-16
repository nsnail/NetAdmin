using FreeSql;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency.Dic;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Sys.Dic.Catalog;

namespace NetAdmin.Application.Services.Sys.Dic;

/// <inheritdoc cref="IDicCatalogService" />
public class DicCatalogService : RepositoryService<TbSysDicCatalog, IDicCatalogService>, IDicCatalogService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicCatalogService" /> class.
    /// </summary>
    public DicCatalogService(Repository<TbSysDicCatalog> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async ValueTask<QueryDicCatalogRsp> Create(CreateDicCatalogReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_parent_node_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryDicCatalogRsp>();
    }

    /// <inheritdoc />
    public async ValueTask<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteCascadeByDatabaseAsync(a => a.Id == req.Id);
        return ret.Count;
    }

    /// <inheritdoc />
    public async ValueTask<PagedQueryRsp<QueryDicCatalogRsp>> PagedQuery(PagedQueryReq<QueryDicCatalogReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryDicCatalogRsp>(req.Page, req.PageSize, total
                                                   , list.Select(x => x.Adapt<QueryDicCatalogRsp>()));
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryDicCatalogRsp>> Query(QueryReq<QueryDicCatalogReq> req)
    {
        var ret = await QueryInternal(req).ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryDicCatalogRsp>());
    }

    /// <inheritdoc />
    public async ValueTask<QueryDicCatalogRsp> Update(UpdateDicCatalogReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_parent_node_does_not_exist);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
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