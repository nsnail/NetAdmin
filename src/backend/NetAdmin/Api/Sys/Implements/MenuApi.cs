using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Api.Sys.IMenuApi" />
public class MenuApi : RepositoryApi<TbSysMenu, IMenuApi>, IMenuApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuApi" /> class.
    /// </summary>
    public MenuApi(Repository<TbSysMenu> repo) //
        : base(repo) { }

    /// <inheritdoc />
    public Task<int> BulkDelete(BulkDelReq req)
    {
        var ret = req.Ids.Sum(x => Delete(new DelReq { Id = x }).Result);
        return Task.FromResult(ret);
    }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public async Task<QueryMenuRsp> Create(CreateMenuReq req)
    {
        var ret = await Repo.InsertAsync(req);
        return ret.Adapt<QueryMenuRsp>();
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        var ret = await Repo.DeleteAsync(a => a.Id == req.Id);
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
        var ret = await Repo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter).ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryMenuRsp>());
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    public async Task<QueryMenuRsp> Update(UpdateMenuReq req)
    {
        if (await Repo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var ret = await Repo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryMenuRsp>();
    }
}