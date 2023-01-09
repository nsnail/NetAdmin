using Furion;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.User;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Extensions;
using NetAdmin.Lang;
using NetAdmin.Repositories;
using NSExt.Extensions;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IUserApi" />
public class UserApi : RepositoryApi<TbSysUser, IUserApi>, IUserApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserApi" /> class.
    /// </summary>
    public UserApi(Repository<TbSysUser> repository) //
        : base(repository) { }

    /// <summary>
    ///     创建用户
    /// </summary>
    [Transaction]
    public async Task<QueryUserRsp> Create(CreateUserReq req)
    {
        var (roles, dept) = await SelectRolesAndDept(req);
        var user = req.Adapt<TbSysUser>();
        user = await Repository.InsertAsync(user);

        var userRoles = req.RoleIds.Select(x => new TbSysUserRole { RoleId = x, UserId = user.Id });
        var effects   = await Repository.Orm.Insert(userRoles).ExecuteAffrowsAsync();
        var ret = effects != req.RoleIds.Count
            ? throw Oops.Oh(Enums.ErrorCodes.Unknown)
            : new Tuple<TbSysUser, TbSysDept, IEnumerable<TbSysRole>>(user, dept, roles).Adapt<QueryUserRsp>();

        return ret;
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> Delete(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public async Task<LoginRsp> Login(LoginReq req)
    {
        var dbUser = await Repository.GetAsync(a => a.UserName == req.UserName &&
                                                    a.Password == req.Password.Pwd().Guid());
        if (dbUser is null) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.USER_NAME_OR_PASSWORD_ERROR);
        }

        if (!dbUser.BitSet.HasFlag(Enums.SysUserBits.Enabled)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.USER_DISABLED);
        }

        var tokenPayload = new Dictionary<string, object> { { nameof(ContextUser), dbUser.Adapt<ContextUser>() } };

        var ret = new LoginRsp { AccessToken = JWTEncryption.Encrypt(tokenPayload) };
        ret.RefreshToken = JWTEncryption.GenerateRefreshToken(ret.AccessToken);

        // 设置响应报文头
        App.HttpContext.Response.Headers[Strings.FLG_ACCESS_TOKEN]   = ret.AccessToken;
        App.HttpContext.Response.Headers[Strings.FLG_X_ACCESS_TOKEN] = ret.RefreshToken;

        return ret;
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        List<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Repository.Orm.Select<TbSysDept>()
                                      .Where(a => a.Id == req.Filter.DeptId)
                                      .AsTreeCte()
                                      .ToListAsync(a => a.Id);
        }

        var list = await Repository.Orm.Select<TbSysUser, TbSysDept>()
                                   .InnerJoin((a, b) => a.DeptId == b.Id)
                                   .WhereDynamicFilter(req.DynamicFilter)
                                   .WhereIf(deptIds != null, (a, b) => deptIds.Contains(a.DeptId))
                                   .WhereIf( //
                                       req.Filter?.RoleId > 0
                                     , (a, b) => Repository.Orm.Select<TbSysUserRole>()
                                                           .Any(aa => aa.UserId == a.Id &&
                                                                      aa.RoleId == req.Filter.RoleId))
                                   .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop
                                                        ,                       req.Order == Enums.Orders.Ascending)
                                   .Page(req.Page, req.PageSize)
                                   .Count(out var total)
                                   .ToListAsync((a, b) => new Tuple<TbSysUser, TbSysDept, IEnumerable<TbSysRole>>(
                                                    a, b
                                                  , Repository.Orm.Select<TbSysUser, TbSysUserRole, TbSysRole>()
                                                              .InnerJoin((aa, bb, cc) => aa.Id     == bb.UserId)
                                                              .InnerJoin((aa, bb, cc) => bb.RoleId == cc.Id)
                                                              .Where((aa,     bb, cc) => aa.Id     == a.Id)
                                                              .ToList((aa,    bb, cc) => cc)));

        return new PagedQueryRsp<QueryUserRsp>(req.Page, req.PageSize, total
                                             , list.Select(x => x.Adapt<QueryUserRsp>()));
    }

    /// <inheritdoc />
    [NonAction]
    public Task<List<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    [Transaction]
    public async Task<QueryUserRsp> Update(UpdateUserReq req)
    {
        var (roles, dept) = await SelectRolesAndDept(req);

        if (await Repository.Orm.Delete<TbSysUserRole>().Where(a => a.UserId == req.Id).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var effects = await Repository.UpdateDiy.SetSource(req)
                                      .IgnoreColumns(a => new { a.Password, a.Token })
                                      .ExecuteAffrowsAsync();

        var userRoles = req.RoleIds.Select(x => new TbSysUserRole { RoleId = x, UserId = req.Id });
        effects += await Repository.Orm.Insert(userRoles).ExecuteAffrowsAsync();

        if (effects != req.RoleIds.Count + 1) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var user = await Repository.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return new Tuple<TbSysUser, TbSysDept, IEnumerable<TbSysRole>>(user, dept, roles).Adapt<QueryUserRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> UserInfo()
    {
        var contextUser = App.User.AsContextUser();
        var dbUser      = await Repository.Where(x => x.Token == contextUser.Token).FirstAsync();
        return dbUser.Adapt<QueryUserRsp>();
    }

    private async Task<(List<TbSysRole> Roles, TbSysDept Dept)> SelectRolesAndDept(CreateUserReq req)
    {
        req.RoleIds = req.RoleIds.Distinct().ToList();

        var roles = await Repository.Orm.Select<TbSysRole>()
                                    .ForUpdate()
                                    .Where(a => req.RoleIds.Contains(a.Id))
                                    .ToListAsync();

        if (roles.Count != req.RoleIds.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "角色不存在");
        }

        var dept = await Repository.Orm.Select<TbSysDept>().ForUpdate().Where(a => a.Id == req.DeptId).ToOneAsync();
        return dept is null ? throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, "部门不存在") : (roles, dept);
    }
}