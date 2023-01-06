using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Dept;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Api.Sys.IDeptApi" />
public class DeptApi : RepositoryApi<TbSysDept, IDeptApi>, IDeptApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeptApi" /> class.
    /// </summary>
    public DeptApi(Repository<TbSysDept> repository) //
        : base(repository) { }

    /// <summary>
    ///     创建部门
    /// </summary>
    public async Task Create(CreateDeptReq req)
    {
        if (req.ParentId != 0 && !await Repository.Select.AnyAsync(a => a.Id == req.ParentId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "父部门不存在");
        }

        await Repository.InsertAsync(req);
    }

    /// <summary>
    ///     删除部门
    /// </summary>
    public async Task<int> Delete(DelReq req)
    {
        if (await Repository.Orm.Select<TbSysUser>().AnyAsync(a => a.DeptId == req.Id)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "该部门下存在用户，不允许删除");
        }

        if (await Repository.Select.AnyAsync(a => a.ParentId == req.Id)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "该部门下存在子部门，不允许删除");
        }

        var ret = await Repository.DeleteAsync(x => x.Id == req.Id);
        return ret;
    }

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQuery(PagedQueryReq<QueryDeptReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     查询部门
    /// </summary>
    public async Task<List<QueryDeptRsp>> Query(QueryReq<QueryDeptReq> req)
    {
        var ret = await Repository.Select.WhereDynamicFilter(req.DynamicFilter)
                                  .WhereDynamic(req.Filter)
                                  .ToTreeListAsync();
        return ret.ConvertAll(x => x.Adapt<QueryDeptRsp>());
    }

    /// <summary>
    ///     更新部门
    /// </summary>
    public async Task<int> Update(UpdateDeptReq req)
    {
        var ret = await Repository.UpdateDiy.SetSource(req).ExecuteAffrowsAsync();
        return ret;
    }
}