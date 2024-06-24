using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserProfileService" />
public sealed class UserProfileService(BasicRepository<Sys_UserProfile, long> rpo) //
    : RepositoryService<Sys_UserProfile, long, IUserProfileService>(rpo), IUserProfileService
{
    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
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
    public Task<int> EditAsync(EditUserProfileReq req)
    {
        return UpdateAsync(req.Adapt<Sys_UserProfile>(), null);
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
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
    public async Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync()
    {
        var ret = await Rpo.Select.Where(a => a.Id == UserToken.Id).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<GetSessionUserAppConfigRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserProfileRsp>> PagedQueryAsync(PagedQueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
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
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
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
    public Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.AppConfig)], null, a => a.Id == UserToken.Id, null, true);
    }

    private ISelect<Sys_UserProfile, Sys_DicContent, Sys_DicContent, Sys_DicContent, Sys_DicContent> QueryInternal(
        QueryReq<QueryUserProfileReq> req)
    {
        #pragma warning disable CA1305,IDE0072
        var ret = Rpo.Orm.Select<Sys_UserProfile, Sys_DicContent, Sys_DicContent, Sys_DicContent, Sys_DicContent>()
                     .LeftJoin((a, b, _, __, ___) =>
                                   a.NationArea.ToString() == b.Value && b.CatalogId == Numbers.ID_DIC_CATALOG_GEO_AREA)
                     .LeftJoin((a, _, c, __, ___) =>
                                   a.CompanyArea.ToString() == c.Value &&
                                   c.CatalogId              == Numbers.ID_DIC_CATALOG_GEO_AREA)
                     .LeftJoin((a, _, __, d, ___) =>
                                   a.HomeArea.ToString() == d.Value && d.CatalogId == Numbers.ID_DIC_CATALOG_GEO_AREA)
                     .LeftJoin((a, _, __, ___, e) => a.EmergencyContactArea.ToString() == e.Value &&
                                                     e.CatalogId == Numbers.ID_DIC_CATALOG_GEO_AREA)
                     .WhereDynamicFilter(req.DynamicFilter);

        return req.Order switch {
                   Orders.None   => ret
                 , Orders.Random => ret.OrderByRandom()
                 , _ => ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending)
                           .OrderByDescending((a, _, __, ___, ____) => a.Id)
               };
        #pragma warning restore CA1305,IDE0072
    }
}