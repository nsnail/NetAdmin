using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDeptService" />
public sealed class DeptService(BasicRepository<Sys_Dept, long> rpo) //
    : RepositoryService<Sys_Dept, long, IDeptService>(rpo), IDeptService
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
    public Task<long> CountAsync(QueryReq<QueryDeptReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">Parent_department_does_not_exist</exception>
    public async Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        req.ThrowIfInvalid();
        if (req.ParentId != 0 && !await Rpo.Select.AnyAsync(a => a.Id == req.ParentId).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        var ret = await Rpo.InsertAsync(req).ConfigureAwait(false);

        return ret.Adapt<QueryDeptRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">该部门下存在用户</exception>
    /// <exception cref="NetAdminInvalidOperationException">该部门下存在子部门</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        if (await Rpo.Orm.Select<Sys_User>().AnyAsync(a => a.DeptId == req.Id).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.该部门下存在用户);
        }

        #pragma warning disable IDE0046
        if (await Rpo.Select.AnyAsync(a => a.ParentId == req.Id).ConfigureAwait(false)) {
            #pragma warning restore IDE0046
            throw new NetAdminInvalidOperationException(Ln.该部门下存在子部门);
        }

        return await Rpo.DeleteAsync(x => x.Id == req.Id).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<QueryDeptRsp> EditAsync(EditDeptReq req)
    {
        #if DBTYPE_SQLSERVER
        return (await UpdateReturnListAsync(req, null).ConfigureAwait(false)).FirstOrDefault()?.Adapt<QueryDeptRsp>();
        #else
        return await UpdateAsync(req, null).ConfigureAwait(false) > 0
            ? await GetAsync(new QueryDeptReq { Id = req.Id }).ConfigureAwait(false)
            : null;
        #endif
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        req.ThrowIfInvalid();
        var ret = await QueryInternal(new QueryReq<QueryDeptReq> { Filter = req }).ToOneAsync().ConfigureAwait(false);
        return ret.Adapt<QueryDeptRsp>();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        req.ThrowIfInvalid();
        return (await QueryInternal(req)
                      #if DBTYPE_SQLSERVER
                      .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                      #endif
                      .ToTreeListAsync()
                      .ConfigureAwait(false)).Adapt<IEnumerable<QueryDeptRsp>>();
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetDeptEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.Enabled)]);
    }

    private ISelect<Sys_Dept> QueryInternal(QueryReq<QueryDeptReq> req, bool asTreeCte = false)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.Id == req.Keywords.Int64Try(0) || a.Name.Contains(req.Keywords) ||
                              a.Summary.Contains(req.Keywords));
        if (asTreeCte) {
            ret = ret.AsTreeCte();
        }

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