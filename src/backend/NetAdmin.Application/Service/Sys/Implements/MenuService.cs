using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Repositories;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="IMenuService" />
public class MenuService : RepositoryService<TbSysMenu, IMenuService>, IMenuService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuService" /> class.
    /// </summary>
    public MenuService(Repository<TbSysMenu> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public Task<int> BulkDelete(BulkDelReq req)
    {
        var ret = req.Ids.Sum(x => Delete(new DelReq { Id = x }).Result);
        return Task.FromResult(ret);
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public async Task<QueryMenuRsp> Create(CreateMenuReq @in)
    {
        var ret = await Rpo.InsertAsync(@in);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Rpo.DeleteAsync(a => a.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQuery(PagedQueryReq<QueryMenuReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async Task<List<QueryMenuRsp>> Query(QueryReq<QueryMenuReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryMenuRsp>());
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    public async Task<QueryMenuRsp> Update(UpdateMenuReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryMenuRsp>();
    }
}