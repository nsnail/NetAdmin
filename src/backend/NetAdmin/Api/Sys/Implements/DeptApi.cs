using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Dept;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Lang;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Api.Sys.IDeptApi" />
public class DeptApi : RepositoryApi<TbSysDept, IDeptApi>, IDeptApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeptApi" /> class.
    /// </summary>
    public DeptApi(Repository<TbSysDept> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     创建部门
    /// </summary>
    public async Task<QueryDeptRsp> Create(CreateDeptReq req)
    {
        if (req.ParentId != 0 && !await Rpo.Select.AnyAsync(a => a.Id == req.ParentId)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.Parent_department_does_not_exist);
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
                Enums.ErrorCodes.InvalidOperation, Str.There_are_users_under_this_department_which_cannot_be_deleted);
        }

        if (await Rpo.Select.AnyAsync(a => a.ParentId == req.Id)) {
            throw Oops.Oh( //
                Enums.ErrorCodes.InvalidOperation
              , Str.There_are_sub_departments_under_this_department_which_cannot_be_deleted);
        }

        var ret = await Rpo.DeleteAsync(x => x.Id == req.Id);
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
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var ret = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return ret.Adapt<QueryDeptRsp>();
    }
}