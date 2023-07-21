using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IDeptService" />
public sealed class DeptService : RepositoryService<Sys_Dept, IDeptService>, IDeptService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeptService" /> class.
    /// </summary>
    public DeptService(Repository<Sys_Dept> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除部门
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
    ///     创建部门
    /// </summary>
    /// <exception cref="NetAdminInvalidOperationException">Parent_department_does_not_exist</exception>
    public async Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.AnyAsync(a => a.Id == req.ParentId)) {
            throw new NetAdminInvalidOperationException(Ln.父节点不存在);
        }

        var ret = await Rpo.InsertAsync(req);

        return ret.Adapt<QueryDeptRsp>();
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    /// <exception cref="NetAdminInvalidOperationException">该部门下存在用户</exception>
    /// <exception cref="NetAdminInvalidOperationException">该部门下存在子部门</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        if (await Rpo.Orm.Select<Sys_User>().AnyAsync(a => a.DeptId == req.Id)) {
            throw new NetAdminInvalidOperationException(Ln.该部门下存在用户);
        }

        #pragma warning disable IDE0046
        if (await Rpo.Select.AnyAsync(a => a.ParentId == req.Id)) {
            #pragma warning restore IDE0046
            throw new NetAdminInvalidOperationException(Ln.该部门下存在子部门);
        }

        return await Rpo.DeleteAsync(x => x.Id == req.Id);
    }

    /// <summary>
    ///     判断部门是否存在
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     获取单个部门
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询部门
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public async Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                           .WhereDynamic(req.Filter)
                           .OrderByDescending(a => a.Sort)
                           .ToTreeListAsync();
        return ret.Adapt<IEnumerable<QueryDeptRsp>>();
    }

    /// <summary>
    ///     更新部门
    /// </summary>
    /// <exception cref="NetAdminUnexpectedException">NetAdminUnexpectedException</exception>
    public async Task<QueryDeptRsp> UpdateAsync(UpdateDeptReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDeptRsp>();
    }
}