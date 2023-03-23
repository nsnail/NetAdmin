using Furion.FriendlyException;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Sms;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;

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
      , BitSet      = a.BitSet
      , UserName    = a.UserName
      , Version     = a.Version
      , CreatedTime = a.CreatedTime
      , Dept        = new TbSysDept { Id = a.Dept.Id, Name = a.Dept.Name }
      , Roles       = a.Roles
    };

    private readonly ISmsService _smsService;

    private readonly IUserProfileService _userProfileService;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserService" /> class.
    /// </summary>
    public UserService(Repository<TbSysUser> rpo, IUserProfileService userProfileService, ISmsService smsService) //
        : base(rpo)
    {
        _userProfileService = userProfileService;
        _smsService         = smsService;
    }

    /// <summary>
    ///     批量删除用户
    /// </summary>
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<bool> CheckMobileAvaliableAsync(CheckMobileAvaliableReq req)
    {
        return !await Rpo.Select.Where(a => a.Mobile == req.Mobile).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<bool> CheckUserNameAvaliableAsync(CheckUserNameAvaliableReq req)
    {
        return !await Rpo.Select.Where(a => a.UserName == req.UserName).AnyAsync();
    }

    /// <summary>
    ///     创建用户
    /// </summary>
    public async Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        await CreateUpdateCheckAsync(req);

        // 主表
        var entity = req.Adapt<TbSysUser>();
        var dbUser = await Rpo.InsertAsync(entity);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));
        await Rpo.SaveManyAsync(entity, nameof(entity.Positions));

        // 档案表
        _ = await _userProfileService.CreateAsync(req.Profile with { Id = dbUser.Id });
        var ret = await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = dbUser.Id } });
        return ret.First();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     分页查询用户
    /// </summary>
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        var list = await (await QueryInternalAsync(req)).Page(req.Page, req.PageSize)
                                                        .Count(out var total)
                                                        .ToListAsync(_selectUserFields);
        return new PagedQueryRsp<QueryUserRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserRsp>>());
    }

    /// <inheritdoc />
    public async Task<LoginRsp> PwdLoginAsync(PwdLoginReq req)
    {
        var pwd = req.Password.Pwd().Guid();

        TbSysUser dbUser;
        #pragma warning disable IDE0045
        if (new MobileAttribute().IsValid(req.Account)) {
            #pragma warning restore IDE0045
            dbUser = await Rpo.GetAsync(a => a.Mobile == req.Account && a.Password == pwd);
        }
        else {
            dbUser = new EmailAddressAttribute().IsValid(req.Account)
                ? await Rpo.GetAsync(a => a.Email    == req.Account && a.Password == pwd)
                : await Rpo.GetAsync(a => a.UserName == req.Account && a.Password == pwd);
        }

        return dbUser is null
            ? throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.User_name_or_password_error)
            : LoginInternal(dbUser);
    }

    /// <summary>
    ///     查询用户
    /// </summary>
    public async Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        var list = await (await QueryInternalAsync(req)).Take(req.Count).ToListAsync(_selectUserFields);
        return list.Adapt<IEnumerable<QueryUserRsp>>();
    }

    /// <summary>
    ///     查询用户档案
    /// </summary>
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return _userProfileService.QueryAsync(req);
    }

    /// <inheritdoc />
    public async Task RegisterAsync(RegisterReq req)
    {
        if (!await _smsService.VerifySmsCodeAsync(req.VerifySmsCodeReq)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Incorrect_SMS_verification_code);
        }

        var createReq = req.Adapt<CreateUserReq>();
        _ = await CreateAsync(createReq);
    }

    /// <inheritdoc />
    public async Task ResetPasswordAsync(ResetPasswordReq req)
    {
        if (!await _smsService.VerifySmsCodeAsync(req.VerifySmsCodeReq)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Incorrect_SMS_verification_code);
        }

        var dbUser = await Rpo.GetAsync(a => a.Mobile == req.VerifySmsCodeReq.DestMobile);
        if (dbUser is null) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.User_does_not_exist);
        }

        dbUser.Password = req.PasswordText.Pwd().Guid();

        _ = await Rpo.UpdateDiy.SetSource(dbUser).ExecuteAffrowsAsync();
    }

    /// <inheritdoc />
    public async Task<LoginRsp> SmsLoginAsync(SmsLoginReq req)
    {
        if (!await _smsService.VerifySmsCodeAsync(req.Adapt<VerifySmsCodeReq>())) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Incorrect_SMS_verification_code);
        }

        var dbUser = await Rpo.GetAsync(a => a.Mobile == req.DestMobile);
        return dbUser is null
            ? throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.User_does_not_exist)
            : LoginInternal(dbUser);
    }

    /// <summary>
    ///     更新用户
    /// </summary>
    public async Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        await CreateUpdateCheckAsync(req);

        // 主表
        var entity = req.Adapt<TbSysUser>();
        _ = await Rpo.UpdateDiy.SetSource(entity).IgnoreColumns(a => new { a.Password, a.Token }).ExecuteAffrowsAsync();

        // 档案表
        _ = await _userProfileService.UpdateAsync(req.Profile);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));
        await Rpo.SaveManyAsync(entity, nameof(entity.Positions));

        var ret = await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = req.Id } });
        return ret.First();
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> UserInfoAsync()
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

    private static LoginRsp LoginInternal(IFieldBitSet dbUser)
    {
        if (!dbUser.BitSet.HasFlag(EntityBase.BitSets.Enabled)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.User_disabled);
        }

        if (!dbUser.BitSet.HasFlag(TbSysUser.UserBits.Activated)) {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Please_contact_the_administrator_to_activate_the_account);
        }

        var tokenPayload = new Dictionary<string, object> { { nameof(ContextUser), dbUser.Adapt<ContextUser>() } };

        var accessToken = JWTEncryption.Encrypt(tokenPayload);
        return new LoginRsp {
                                AccessToken  = accessToken
                              , RefreshToken = JWTEncryption.GenerateRefreshToken(accessToken)
                            };
    }

    private async Task CreateUpdateCheckAsync(CreateUserReq req)
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

    private async Task<ISelect<TbSysUser>> QueryInternalAsync(QueryReq<QueryUserReq> req)
    {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Rpo.Orm.Select<TbSysDept>()
                               .Where(a => a.Id == req.Filter.DeptId)
                               .AsTreeCte()
                               .ToListAsync(a => a.Id);
        }

        return Rpo.Select.Include(a => a.Dept)
                  .IncludeMany(a => a.Roles.Select(b => new TbSysRole { Id         = b.Id, Name = b.Name }))
                  .IncludeMany(a => a.Positions.Select(b => new TbSysPosition { Id = b.Id, Name = b.Name }))
                  .WhereDynamicFilter(req.DynamicFilter)
                  .WhereIf(deptIds != null, a => deptIds.Contains(a.DeptId))
                  .WhereIf( //
                      req.Filter?.Id > 0, a => a.Id == req.Filter.Id)
                  .WhereIf( //
                      req.Filter?.RoleId > 0, a => a.Roles.Any(b => b.Id == req.Filter.RoleId))
                  .WhereIf( //
                      req.Filter?.PositionId              > 0, a => a.Positions.Any(b => b.Id == req.Filter.PositionId))
                  .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending);
    }
}