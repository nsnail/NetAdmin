using FreeSql;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency.Dic;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Sys.Dic.Content;

namespace NetAdmin.Application.Services.Sys.Dic;

/// <inheritdoc cref="IDicContentService" />
public class DicContentService : RepositoryService<TbSysDicContent, IDicContentService>, IDicContentService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicContentService" /> class.
    /// </summary>
    public DicContentService(Repository<TbSysDicContent> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async ValueTask<int> BulkDelete(BulkDelReq req)
    {
        var sum = 0;
        foreach (var id in req.Ids) {
            sum += await Delete(new DelReq { Id = id });
        }

        return sum;
    }

    /// <inheritdoc />
    public async ValueTask<QueryDicContentRsp> Create(CreateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<TbSysDicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Dictionary_directory_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryDicContentRsp>();
    }

    /// <inheritdoc />
    public async ValueTask<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    public async ValueTask<PagedQueryRsp<QueryDicContentRsp>> PagedQuery(PagedQueryReq<QueryDicContentReq> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<QueryDicContentRsp>(req.Page, req.PageSize, total
                                                   , list.Select(x => x.Adapt<QueryDicContentRsp>()));
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryDicContentRsp>> Query(QueryReq<QueryDicContentReq> req)
    {
        var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryDicContentRsp>());
    }

    /// <inheritdoc />
    public async ValueTask<QueryDicContentRsp> Update(UpdateDicContentReq req)
    {
        if (!await Rpo.Orm.Select<TbSysDicCatalog>().Where(a => a.Id == req.CatalogId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Dictionary_directory_does_not_exist);
        }

        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDicContentRsp>();
    }

    private ISelect<TbSysDicContent> QueryInternal(QueryReq<QueryDicContentReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                     .OrderByDescending(a => a.Id);
        return ret;
    }
}