using System.Linq.Expressions;
using FreeSql;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Mapster;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NSExt.Extensions;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IUserService" />
public class UserService : RepositoryService<TbSysUser, IUserService>, IUserService
{
    private readonly Expression<Func<TbSysUser, TbSysUser>> _selectUserFields = a => new TbSysUser {
        Id          = a.Id
      , Positions   = a.Positions
      , Avatar      = a.Avatar
      , Email       = a.Email
      , Mobile      = a.Mobile
      , Summary     = a.Summary
      , BitSet      = a.BitSet
      , UserName    = a.UserName
      , Version     = a.Version
      , CreatedTime = a.CreatedTime
      , Dept        = new TbSysDept { Id = a.Dept.Id, Name = a.Dept.Name }
      , Roles       = a.Roles
    };

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserService" /> class.
    /// </summary>
    public UserService(Repository<TbSysUser> rpo) //
        : base(rpo) { }

    /// <summary>
    ///     批量删除用户
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
    ///     创建用户
    /// </summary>
    public async Task<QueryUserRsp> Create(CreateUserReq req)
    {
        await CreateUpdateCheck(req);

        // 主表
        var entity = req.Adapt<TbSysUser>();
        var dbUser = await Rpo.InsertAsync(entity);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));
        await Rpo.SaveManyAsync(entity, nameof(entity.Positions));

        var ret = await Query(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = dbUser.Id } });
        return ret.First();
    }

    /// <inheritdoc />
    public Task<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<LoginRsp> Login(LoginReq req)
    {
        var dbUser = await Rpo.GetAsync(a => a.UserName == req.UserName && a.Password == req.Password.Pwd().Guid());
        if (dbUser is null) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.User_name_or_password_error);
        }

        if (!dbUser.BitSet.HasFlag(EntityBase.BitSets.Enabled)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.User_disabled);
        }

        var tokenPayload = new Dictionary<string, object> { { nameof(ContextUser), dbUser.Adapt<ContextUser>() } };

        var accessToken = JWTEncryption.Encrypt(tokenPayload);
        var ret = new LoginRsp {
                                   AccessToken  = accessToken
                                 , RefreshToken = JWTEncryption.GenerateRefreshToken(accessToken)
                               };

        return ret;
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQuery(PagedQueryReq<QueryUserReq> req)
    {
        var list = await (await QueryInternal(req)).Page(req.Page, req.PageSize)
                                                   .Count(out var total)
                                                   .ToListAsync(_selectUserFields);
        return new PagedQueryRsp<QueryUserRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserRsp>>());
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    public async Task<IEnumerable<QueryUserRsp>> Query(QueryReq<QueryUserReq> req)
    {
        var list = await (await QueryInternal(req)).Take(Numbers.QUERY_LIMIT).ToListAsync(_selectUserFields);
        return list.Adapt<IEnumerable<QueryUserRsp>>();
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    public async Task<QueryUserRsp> Update(UpdateUserReq req)
    {
        await CreateUpdateCheck(req);

        // 主表
        var entity = req.Adapt<TbSysUser>();
        await Rpo.UpdateDiy.SetSource(entity).IgnoreColumns(a => new { a.Password, a.Token }).ExecuteAffrowsAsync();

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));
        await Rpo.SaveManyAsync(entity, nameof(entity.Positions));

        var ret = await Query(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = req.Id } });
        return ret.First();
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> UserInfo()
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

    private async Task CreateUpdateCheck(CreateUserReq req)
    {
        // 检查角色是否存在、有效
        var roles = await Rpo.Orm.Select<TbSysRole>()
                             .ForUpdate()
                             .Where(a => req.RoleIds.Contains(a.Id) &&
                                         (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1)
                             .ToListAsync(a => a.Id);
        if (roles.Count != req.RoleIds.Count) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.The_character_does_not_exist);
        }

        // 检查岗位是否存在、有效
        var positions = await Rpo.Orm.Select<TbSysPosition>()
                                 .ForUpdate()
                                 .Where(a => req.PositionIds.Contains(a.Id) &&
                                             (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1)
                                 .ToListAsync(a => a.Id);
        if (positions.Count != req.PositionIds.Count) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Position_does_not_exist);
        }

        // 检查部门是否存在、有效
        var dept = await Rpo.Orm.Select<TbSysDept>()
                            .ForUpdate()
                            .Where(a => req.DeptId == a.Id && (a.BitSet & (long)EntityBase.BitSets.Enabled) == 1)
                            .ToListAsync(a => a.Id);

        if (dept.Count != 1) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.The_department_does_not_exist);
        }
    }

    private async Task<ISelect<TbSysUser>> QueryInternal(QueryReq<QueryUserReq> req)
    {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Rpo.Orm.Select<TbSysDept>()
                               .Where(a => a.Id == req.Filter.DeptId)
                               .AsTreeCte()
                               .ToListAsync(a => a.Id);
        }

        var ret = Rpo.Select.Include(a => a.Dept)
                     .IncludeMany(a => a.Roles.Select(b => new TbSysRole { Id         = b.Id, Name = b.Name }))
                     .IncludeMany(a => a.Positions.Select(b => new TbSysPosition { Id = b.Id, Name = b.Name }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf(deptIds != null, a => deptIds.Contains(a.DeptId))
                     .WhereIf( //
                         req.Filter?.Id > 0, a => a.Id == req.Filter.Id)
                     .WhereIf( //
                         req.Filter?.RoleId > 0, a => a.Roles.Any(b => b.Id == req.Filter.RoleId))
                     .WhereIf( //
                         req.Filter?.PositionId > 0
                       , a => a.Positions.Any(b => b.Id == req.Filter.PositionId))
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending);

        return ret;
    }
}