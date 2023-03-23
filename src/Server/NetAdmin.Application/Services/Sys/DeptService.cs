using Furion.FriendlyException;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IDeptService" />
public class DeptService : RepositoryService<TbSysDept, IDeptService>, IDeptService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeptService" /> class.
    /// </summary>
    public DeptService(Repository<TbSysDept> rpo) //
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
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.AnyAsync(a => a.Id == req.ParentId)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Parent_department_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);

        return ret.Adapt<QueryDeptRsp>();
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<int> DeleteAsync(DelReq req)
    {
        #pragma warning disable IDE0046
        if (await Rpo.Orm.Select<TbSysUser>().AnyAsync(a => a.DeptId == req.Id)) {
            #pragma warning restore IDE0046
            throw Oops.Oh( //
                Enums.RspCodes.InvalidOperation, Ln.There_are_users_under_this_department_which_cannot_be_deleted);
        }

        return await Rpo.Select.AnyAsync(a => a.ParentId == req.Id)
            ? throw Oops.Oh( //
                Enums.RspCodes.InvalidOperation
              , Ln.There_are_sub_departments_under_this_department_which_cannot_be_deleted)
            : await Rpo.DeleteAsync(x => x.Id == req.Id);
    }

    /// <inheritdoc />
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
    /// <exception cref="AppFriendlyException">AppFriendlyException</exception>
    public async Task<QueryDeptRsp> UpdateAsync(UpdateDeptReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDeptRsp>();
    }
}