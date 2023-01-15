using Furion;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IUserService" />
public class UserService : RepositoryService<TbSysUser, IUserService>, IUserService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserService" /> class.
    /// </summary>
    public UserService(Repository<TbSysUser> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     创建用户
    /// </summary>
    public async ValueTask<QueryUserRsp> Create(CreateUserReq req)
    {
        var (roles, dept) = await SelectRolesAndDept(req);
        var user = req.Adapt<TbSysUser>();
        user = await Rpo.InsertAsync(user);

        var userRoles = req.RoleIds.Select(x => new TbSysUserRole { RoleId = x, UserId = user.Id });
        var effects   = await Rpo.Orm.Insert(userRoles).ExecuteAffrowsAsync();
        var ret = effects != req.RoleIds.Count
            ? throw Oops.Oh(Enums.ErrorCodes.Unknown)
            : new Tuple<TbSysUser, TbSysDept, IEnumerable<TbSysRole>>(user, dept, roles).Adapt<QueryUserRsp>();

        return ret;
    }

    /// <inheritdoc />
    public ValueTask<int> Delete(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async ValueTask<LoginRsp> Login(LoginReq req)
    {
        var dbUser = await Rpo.GetAsync(a => a.UserName == req.UserName && a.Password == req.Password.Pwd().Guid());
        if (dbUser is null) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.User_name_or_password_error);
        }

        if (!dbUser.BitSet.HasFlag(EntityBase.BitSets.Enabled)) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.User_disabled);
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
    public async ValueTask<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        List<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Rpo.Orm.Select<TbSysDept>()
                               .Where(a => a.Id == req.Filter.DeptId)
                               .AsTreeCte()
                               .ToListAsync(a => a.Id);
        }

        var list = await Rpo.Select.Include(a => a.Dept)
                            .IncludeMany(a => a.Roles)
                            .WhereDynamicFilter(req.DynamicFilter)
                            .WhereIf(deptIds != null, a => deptIds.Contains(a.DeptId))
                            .WhereIf( //
                                req.Filter?.RoleId > 0
                              , a => a.Roles.Any(b => b.Id == req.Filter.RoleId))
                            .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                            .Page(req.Page, req.PageSize)
                            .Count(out var total)
                            .ToListAsync();

        return new PagedQueryRsp<QueryUserRsp>(req.Page, req.PageSize, total
                                             , list.Select(x => x.Adapt<QueryUserRsp>()));
    }

    /// <inheritdoc />
    public ValueTask<List<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    public async ValueTask<QueryUserRsp> Update(UpdateUserReq req)
    {
        var (roles, dept) = await SelectRolesAndDept(req);

        if (await Rpo.Orm.Delete<TbSysUserRole>().Where(a => a.UserId == req.Id).ExecuteAffrowsAsync() <= 0) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var effects = await Rpo.UpdateDiy.SetSource(req)
                               .IgnoreColumns(a => new { a.Password, a.Token })
                               .ExecuteAffrowsAsync();

        var userRoles = req.RoleIds.Select(x => new TbSysUserRole { RoleId = x, UserId = req.Id });
        effects += await Rpo.Orm.Insert(userRoles).ExecuteAffrowsAsync();

        if (effects != req.RoleIds.Count + 1) {
            throw Oops.Oh(Enums.ErrorCodes.Unknown);
        }

        var user = await Rpo.Select.Where(a => a.Id == req.Id).ToOneAsync();
        return new Tuple<TbSysUser, TbSysDept, IEnumerable<TbSysRole>>(user, dept, roles).Adapt<QueryUserRsp>();
    }

    /// <inheritdoc />
    public async ValueTask<QueryUserRsp> UserInfo()
    {
        var dbUser = await Rpo.Where(a => a.Token == User.Token && (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1)
                              .Include(a => a.Dept)
                              .IncludeMany( //
                                  a => a.Roles, then =>
                                      then.Where(a => (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1)
                                          .IncludeMany( //
                                              a => a.Menus
                                            , menuThen =>
                                                  menuThen.Where(
                                                      a => (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1))
                                          .IncludeMany( //
                                              a => a.Depts
                                            , deptThen =>
                                                  deptThen.Where(
                                                      a => (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1))
                                          .IncludeMany(a => a.Apis))
                              .ToOneAsync();
        return dbUser.Adapt<QueryUserRsp>();
    }

    private async ValueTask<(IEnumerable<TbSysRole> Roles, TbSysDept Dept)> SelectRolesAndDept(CreateUserReq req)
    {
        req.RoleIds = req.RoleIds.Distinct().ToList();

        var roles = await Rpo.Orm.Select<TbSysRole>().ForUpdate().Where(a => req.RoleIds.Contains(a.Id)).ToListAsync();

        if (roles.Count != req.RoleIds.Count) {
            throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_character_does_not_exist);
        }

        var dept = await Rpo.Orm.Select<TbSysDept>().ForUpdate().Where(a => a.Id == req.DeptId).ToOneAsync();
        return dept is null
            ? throw Oops.Oh(Enums.ErrorCodes.InvalidOperation, Str.The_department_does_not_exist)
            : (roles, dept);
    }
}