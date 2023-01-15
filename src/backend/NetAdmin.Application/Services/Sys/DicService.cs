using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IDicService" />
public class DicService : RepositoryService<TbSysDic, IDicService>, IDicService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DicService" /> class.
    /// </summary>
    public DicService(Repository<TbSysDic> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public async ValueTask<QueryDicRsp> Create(CreateDicReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.Where(a => a.Id == req.ParentId).ForUpdate().AnyAsync()) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_parent_node_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);
        return ret.Adapt<QueryDicRsp>();
    }

    /// <inheritdoc />
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ValueTask<PagedQueryRsp<QueryDicRsp>> PagedQuery(PagedQueryReq<QueryDicReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async ValueTask<List<QueryDicRsp>> Query(QueryReq<QueryDicReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryDicRsp>());
    }

    /// <inheritdoc />
    public ValueTask<QueryDicRsp> Update(UpdateDicReq req)
    {
        throw new NotImplementedException();
    }
}