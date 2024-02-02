using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using DataType = FreeSql.DataType;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserProfileService" />
public sealed class UserProfileService(DefaultRepository<Sys_UserProfile> rpo) //
    : RepositoryService<Sys_UserProfile, IUserProfileService>(rpo), IUserProfileService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item).ConfigureAwait(false);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<QueryUserProfileRsp> CreateAsync(CreateUserProfileReq req)
    {
        req.ThrowIfInvalid();
        var entity = req.Adapt<Sys_UserProfile>();
        var ret    = await Rpo.InsertAsync(entity).ConfigureAwait(false);
        return ret.Adapt<QueryUserProfileRsp>();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return Rpo.DeleteAsync(a => a.Id == req.Id);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryUserProfileRsp> GetAsync(QueryUserProfileReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryUserProfileReq> { Filter = req })
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryUserProfileRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserProfileRsp>> PagedQueryAsync(PagedQueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         .Count(out var total)
                         .ToListAsync((a, b, c, d, e) => new {
                                                                 a
                                                               , b = new { b.Key, b.Value }
                                                               , c = new { c.Key, c.Value }
                                                               , d = new { d.Key, d.Value }
                                                               , e = new { e.Key, e.Value }
                                                             })
                         .ConfigureAwait(false);

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

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserProfileRsp>> QueryAsync(QueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        .Take(req.Count)
                        .ToListAsync((a, b, c, d, e) => new {
                                                                a
                                                              , b = new { b.Key, b.Value }
                                                              , c = new { c.Key, c.Value }
                                                              , d = new { d.Key, d.Value }
                                                              , e = new { e.Key, e.Value }
                                                            })
                        .ConfigureAwait(false);
        return ret.ConvertAll(x => x.a.Adapt<QueryUserProfileRsp>() with {
                                                                             NationArea
                                                                             = x.b.Key == null
                                                                                 ? null
                                                                                 : x.b.Adapt<QueryDicContentRsp>()
                                                                           , CompanyArea
                                                                             = x.c.Key == null
                                                                                 ? null
                                                                                 : x.c.Adapt<QueryDicContentRsp>()
                                                                           , HomeArea
                                                                             = x.d.Key == null
                                                                                 ? null
                                                                                 : x.d.Adapt<QueryDicContentRsp>()
                                                                           , EmergencyContactArea
                                                                             = x.e.Key == null
                                                                                 ? null
                                                                                 : x.e.Adapt<QueryDicContentRsp>()
                                                                         });
    }

    /// <inheritdoc />
    public async Task<QueryUserProfileRsp> UpdateAsync(UpdateUserProfileReq req)
    {
        req.ThrowIfInvalid();
        var entity = req.Adapt<Sys_UserProfile>();
        if (Rpo.Orm.Ado.DataType == DataType.Sqlite) {
            return await UpdateForSqliteAsync(entity).ConfigureAwait(false) as QueryUserProfileRsp;
        }

        var ret = await Rpo.UpdateDiy.SetSource(entity).ExecuteUpdatedAsync().ConfigureAwait(false);
        return ret.FirstOrDefault()?.Adapt<QueryUserProfileRsp>();
    }

    /// <inheritdoc />
    protected override async Task<Sys_UserProfile> UpdateForSqliteAsync(Sys_UserProfile req)
    {
        return await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync().ConfigureAwait(false) <= 0
            ? null
            : await GetAsync(new QueryUserProfileReq { Id = req.Id }).ConfigureAwait(false);
    }

    private ISelect<Sys_UserProfile, Sys_DicContent, Sys_DicContent, Sys_DicContent, Sys_DicContent> QueryInternal(
        QueryReq<QueryUserProfileReq> req)
    {
        #pragma warning disable CA1305
        return Rpo.Orm.Select<Sys_UserProfile, Sys_DicContent, Sys_DicContent, Sys_DicContent, Sys_DicContent>()
                  .LeftJoin((a, b, _, __, ___) =>
                                a.NationArea.ToString() == b.Value && b.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .LeftJoin((a, _, c, __, ___) =>
                                a.CompanyArea.ToString() == c.Value && c.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .LeftJoin((a, _, __, d, ___) =>
                                a.HomeArea.ToString() == d.Value && d.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .LeftJoin((a, _, __, ___, e) => a.EmergencyContactArea.ToString() == e.Value &&
                                                  e.CatalogId == Numbers.DIC_CATALOG_ID_GEO_AREA)
                  .WhereDynamicFilter(req.DynamicFilter)
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                  .OrderByDescending((a, _, __, ___, ____) => a.Id);
        #pragma warning restore CA1305
    }
}