using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserProfileService" />
public sealed class UserProfileService : RepositoryService<Sys_UserProfile, IUserProfileService>, IUserProfileService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserProfileService" /> class.
    /// </summary>
    public UserProfileService(Repository<Sys_UserProfile> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除用户档案
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
    ///     创建用户档案
    /// </summary>
    public async Task<QueryUserProfileRsp> CreateAsync(CreateUserProfileReq req)
    {
        var entity = req.Adapt<Sys_UserProfile>();
        var ret    = await Rpo.InsertAsync(entity);
        return ret.Adapt<QueryUserProfileRsp>();
    }

    /// <summary>
    ///     删除用户档案
    /// </summary>
    public Task<int> DeleteAsync(DelReq req)
    {
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserProfileReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryUserProfileRsp> GetAsync(QueryUserProfileReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询用户档案
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserProfileRsp>> PagedQueryAsync(PagedQueryReq<QueryUserProfileReq> req)
    {
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync((a, b, c, d, e) =>
                                          new {
                                                  a
                                                , b = new { b.Key, b.Value }
                                                , c = new { c.Key, c.Value }
                                                , d = new { d.Key, d.Value }
                                                , e = new { e.Key, e.Value }
                                              });

        return new PagedQueryRsp<QueryUserProfileRsp>(req.Page, req.PageSize, total
                                                    , list.ConvertAll(
                                                          x => x.a.Adapt<QueryUserProfileRsp>() with {
                                                                   NationArea = x.b.Adapt<QueryDicContentRsp>()
                                                                 , CompanyArea = x.c.Adapt<QueryDicContentRsp>()
                                                                 , HomeArea = x.d.Adapt<QueryDicContentRsp>()
                                                                 , EmergencyContactArea
                                                                   = x.e.Adapt<QueryDicContentRsp>()
                                                               }));
    }

    /// <summary>
    ///     查询用户档案
    /// </summary>
    public async Task<IEnumerable<QueryUserProfileRsp>> QueryAsync(QueryReq<QueryUserProfileReq> req)
    {
        var ret = await QueryInternal(req)
                        .Take(req.Count)
                        .ToListAsync((a, b, c, d, e) =>
                                         new {
                                                 a
                                               , b = new { b.Key, b.Value }
                                               , c = new { c.Key, c.Value }
                                               , d = new { d.Key, d.Value }
                                               , e = new { e.Key, e.Value }
                                             });
        return ret.ConvertAll(x => x.a.Adapt<QueryUserProfileRsp>() with {
                                                                             NationArea
                                                                             = x.b.Adapt<QueryDicContentRsp>()
                                                                           , CompanyArea
                                                                             = x.c.Adapt<QueryDicContentRsp>()
                                                                           , HomeArea = x.d.Adapt<QueryDicContentRsp>()
                                                                           , EmergencyContactArea
                                                                             = x.e.Adapt<QueryDicContentRsp>()
                                                                         });
    }

    /// <summary>
    ///     更新用户档案
    /// </summary>
    public async Task<QueryUserProfileRsp> UpdateAsync(UpdateUserProfileReq req)
    {
        var entity = req.Adapt<Sys_UserProfile>();

        var ret = await Rpo.UpdateDiy.SetSource(entity).ExecuteUpdatedAsync();
        return ret.FirstOrDefault()?.Adapt<QueryUserProfileRsp>();
    }

    private ISelect<Sys_UserProfile, Sys_DicContent, Sys_DicContent, Sys_DicContent, Sys_DicContent> QueryInternal(
        QueryReq<QueryUserProfileReq> req)
    {
        return Rpo.Orm.Select<Sys_UserProfile, Sys_DicContent, Sys_DicContent, Sys_DicContent, Sys_DicContent>()
                  .LeftJoin((a, b, _, __, ___) => a.NationArea.ToString() == b.Value &&
                                                  b.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .LeftJoin((a, _, c, __, ___) => a.CompanyArea.ToString() == c.Value &&
                                                  c.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .LeftJoin((a, _, __, d, ___) => a.HomeArea.ToString() == d.Value &&
                                                  d.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .LeftJoin((a, _, __, ___, e) => a.EmergencyContactArea.ToString() == e.Value &&
                                                  e.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .WhereDynamicFilter(req.DynamicFilter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                  .OrderByDescending((a, _, __, ___, ____) => a.Id);
    }
}