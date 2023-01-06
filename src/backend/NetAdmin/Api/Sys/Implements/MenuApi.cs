using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Menu;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Api.Sys.IMenuApi" />
public class MenuApi : RepositoryApi<TbSysMenu, IMenuApi>, IMenuApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuApi" /> class.
    /// </summary>
    public MenuApi(Repository<TbSysMenu> repository) //
        : base(repository) { }

    /// <summary>
    ///     创建菜单
    /// </summary>
    public Task Create(CreateMenuReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     删除菜单
    /// </summary>
    public Task<int> Delete(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<MenuInfo>> PagedQuery(PagedQueryReq<MenuInfo> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询菜单
    /// </summary>
    public async Task<List<MenuInfo>> Query(QueryReq<MenuInfo> req)
    {
        var ret = await Repository.Select.WhereDynamicFilter(req.DynamicFilter)
                                  .WhereDynamic(req.Filter)
                                  .ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<MenuInfo>());
    }

    /// <summary>
    ///     更新菜单
    /// </summary>
    public Task<int> Update(NopReq req)
    {
        throw new NotImplementedException();
    }
}