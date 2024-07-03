using CsvHelper;
using Microsoft.Net.Http.Headers;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IRoleService" />
public sealed class RoleService(BasicRepository<Sys_Role, long> rpo) //
    : RepositoryService<Sys_Role, long, IRoleService>(rpo), IRoleService
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
    public Task<long> CountAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public async Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        req.ThrowIfInvalid();
        var entity = req.Adapt<Sys_Role>();
        var ret    = await Rpo.InsertAsync(entity).ConfigureAwait(false);

        await Rpo.SaveManyAsync(entity, nameof(entity.Depts)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis)).ConfigureAwait(false);

        entity = entity with { Id = ret.Id };
        return entity.Adapt<QueryRoleRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Users_exist_under_this_role_and_deletion_is_not_allowed</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        return await Rpo.Orm.Select<Sys_UserRole>().ForUpdate().AnyAsync(a => a.RoleId == req.Id).ConfigureAwait(false)
            ? throw new NetAdminInvalidOperationException(Ln.该角色下存在用户)
            : await Rpo.DeleteAsync(a => a.Id == req.Id).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<QueryRoleRsp> EditAsync(EditRoleReq req)
    {
        req.ThrowIfInvalid();
        var entity = req.Adapt<Sys_Role>();
        _ = await Rpo.UpdateAsync(entity).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Depts)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Menus)).ConfigureAwait(false);
        await Rpo.SaveManyAsync(entity, nameof(entity.Apis)).ConfigureAwait(false);

        return (await QueryAsync(new QueryReq<QueryRoleReq> { Filter = new QueryRoleReq { Id = req.Id } })
            .ConfigureAwait(false)).First();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public async Task<IActionResult> ExportAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        var data = await QueryInternal(req)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Take(Numbers.MAX_LIMIT_EXPORT)
                         .ToListAsync()
                         .ConfigureAwait(false);
        var list   = data.Adapt<List<ExportRoleRsp>>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        var csv    = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteHeader<ExportRoleRsp>();
        await csv.NextRecordAsync().ConfigureAwait(false);

        foreach (var item in list) {
            csv.WriteRecord(item);
            await csv.NextRecordAsync().ConfigureAwait(false);
        }

        await csv.FlushAsync().ConfigureAwait(false);
        _ = stream.Seek(0, SeekOrigin.Begin);

        App.HttpContext.Response.Headers.ContentDisposition
            = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT) {
                  FileNameStar = $"{Ln.角色导出}_{DateTime.Now:yyyy.MM.dd-HH.mm.ss}.csv"
              }.ToString();
        return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

    /// <inheritdoc />
    public async Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryRoleReq> { Filter = req }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryRoleRsp>();
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        var list = await QueryInternal(req)
                         .Page(req.Page, req.PageSize)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Count(out var total)
                         .ToListAsync()
                         .ConfigureAwait(false);

        return new PagedQueryRsp<QueryRoleRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryRoleRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(req)
                        #if DBTYPE_SQLSERVER
                        .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                        #endif
                        .ToListAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryRoleRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.DisplayDashboard)]);
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetRoleEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.Enabled)]);
    }

    /// <inheritdoc />
    public Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.IgnorePermissionControl)]);
    }

    private ISelect<Sys_Role> QueryInternal(QueryReq<QueryRoleReq> req)
    {
        var ret = Rpo.Select.IncludeMany(a => a.Depts.Select(b => new Sys_Dept { Id = b.Id }))
                     .IncludeMany(a => a.Menus.Select(b => new Sys_Menu { Id        = b.Id }))
                     .IncludeMany(a => a.Apis.Select(b => new Sys_Api { Id          = b.Id }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.Id == req.Keywords.Int64Try(0) || a.Name.Contains(req.Keywords) ||
                              a.Summary.Contains(req.Keywords));
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.Sort), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Sort);
        }

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}