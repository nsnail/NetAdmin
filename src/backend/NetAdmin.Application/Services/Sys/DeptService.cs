using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.DbMaps.Sys;
using NetAdmin.DataContract.Dto.Dependency;
using NetAdmin.DataContract.Dto.Sys.Dept;

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
    public async Task<int> BulkDelete(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await Delete(item);
        }

        return sum;
    }

    /// <summary>
    ///     创建部门
    /// </summary>
    public async Task<QueryDeptRsp> Create(CreateDeptReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.AnyAsync(a => a.Id == req.ParentId)) {
            throw Oops.Oh(Enums.StatusCodes.InvalidOperation, Ln.Parent_department_does_not_exist);
        }

        var ret = await Rpo.InsertAsync(req);

        return ret.Adapt<QueryDeptRsp>();
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        if (await Rpo.Orm.Select<TbSysUser>().AnyAsync(a => a.DeptId == req.Id)) {
            throw Oops.Oh( //
                Enums.StatusCodes.InvalidOperation, Ln.There_are_users_under_this_department_which_cannot_be_deleted);
        }

        if (await Rpo.Select.AnyAsync(a => a.ParentId == req.Id)) {
            throw Oops.Oh( //
                Enums.StatusCodes.InvalidOperation
              , Ln.There_are_sub_departments_under_this_department_which_cannot_be_deleted);
        }

        var ret = await Rpo.DeleteAsync(x => x.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQuery(PagedQueryReq<QueryDeptReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public async Task<List<QueryDeptRsp>> Query(QueryReq<QueryDeptReq> req)
    {
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                           .WhereDynamic(req.Filter)
                           .OrderByDescending(a => a.Sort)
                           .ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryDeptRsp>());
    }

    /// <summary>
    ///     更新部门
    /// </summary>
    public async Task<QueryDeptRsp> Update(UpdateDeptReq req)
    {
        if (await Rpo.UpdateDiy.SetSource(req).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.StatusCodes.InvalidOperation);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDeptRsp>();
    }
}