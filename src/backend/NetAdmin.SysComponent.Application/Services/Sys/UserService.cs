using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Events.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserService" />
public sealed class UserService(Repository<Sys_User> rpo, IUserProfileService userProfileService
                              , IVerifyCodeService   verifyCodeService
                              , IEventPublisher      eventPublisher) : RepositoryService<Sys_User, IUserService>(rpo)
                                                                     , IUserService
{
    private readonly Expression<Func<Sys_User, Sys_User>> _selectUserFields = a => new Sys_User {
        Id          = a.Id
      , Avatar      = a.Avatar
      , Email       = a.Email
      , Mobile      = a.Mobile
      , Enabled     = a.Enabled
      , UserName    = a.UserName
      , Summary     = a.Summary
      , Version     = a.Version
      , CreatedTime = a.CreatedTime
      , Dept        = new Sys_Dept { Id = a.Dept.Id, Name = a.Dept.Name }
      , Roles       = a.Roles
    };

    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var sum = 0;
        foreach (var item in req.Items) {
            sum += await DeleteAsync(item);
        }

        return sum;
    }

    /// <inheritdoc />
    public async Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        return !await Rpo.Select.Where(a => a.Mobile == req.Mobile && a.Id != req.Id).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        return !await Rpo.Select.Where(a => a.UserName == req.UserName && a.Id != req.Id).AnyAsync();
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        await CreateUpdateCheckAsync(req);

        // 主表
        var entity = req.Adapt<Sys_User>();
        var dbUser = await Rpo.InsertAsync(entity);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));

        // 档案表
        _ = await userProfileService.CreateAsync(req.Profile with { Id = dbUser.Id });
        var ret = await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = dbUser.Id } });
        return ret.First();
    }

    /// <inheritdoc />
    public async Task<int> DeleteAsync(DelReq req)
    {
        var effect = 0;

        // 删除主表
        effect += await Rpo.DeleteAsync(req.Id);

        // 删除分表
        effect += await Rpo.Orm.Delete<Sys_UserRole>(new { UserId = req.Id }).ExecuteAffrowsAsync();

        // 删除档案表
        effect += await userProfileService.DeleteAsync(req);

        return effect;
    }

    /// <inheritdoc />
    public async Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        return await (await QueryInternalAsync(req)).AnyAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    public async Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        var ret = await (await QueryInternalAsync(new QueryReq<QueryUserReq> { Filter = req })).ToOneAsync();
        return ret.Adapt<QueryUserRsp>();
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> GetForUpdateAsync(QueryUserReq req)
    {
        // ReSharper disable once MethodHasAsyncOverload
        return (await QueryInternal(new QueryReq<QueryUserReq> { Filter = req }).ForUpdate().ToOneAsync())
            .Adapt<QueryUserRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">用户名或密码错误</exception>
    public async Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        var pwd = req.Password.Pwd().Guid();

        Sys_User dbUser;
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

        return dbUser == null ? throw new NetAdminInvalidOperationException(Ln.用户名或密码错误) : LoginInternal(dbUser);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">验证码不正确</exception>
    /// <exception cref="NetAdminInvalidOperationException">用户不存在</exception>
    public async Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        if (!await verifyCodeService.VerifyAsync(req.Adapt<VerifySmsCodeReq>())) {
            throw new NetAdminInvalidOperationException(Ln.验证码不正确);
        }

        var dbUser = await Rpo.GetAsync(a => a.Mobile == req.DestDevice);
        return dbUser == null ? throw new NetAdminInvalidOperationException(Ln.用户不存在) : LoginInternal(dbUser);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        var list = await (await QueryInternalAsync(req)).Page(req.Page, req.PageSize)
                                                        .Count(out var total)
                                                        .ToListAsync(_selectUserFields);
        return new PagedQueryRsp<QueryUserRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        var list = await (await QueryInternalAsync(req)).Take(req.Count).ToListAsync(_selectUserFields);
        return list.Adapt<IEnumerable<QueryUserRsp>>();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return userProfileService.QueryAsync(req);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">验证码不正确</exception>
    public async Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
    {
        if (!await verifyCodeService.VerifyAsync(req.VerifySmsCodeReq)) {
            throw new NetAdminInvalidOperationException(Ln.验证码不正确);
        }

        var createReq = req.Adapt<CreateUserReq>() with { Profile = new CreateUserProfileReq() };
        return (await CreateAsync(createReq)).Adapt<UserInfoRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">验证码不正确</exception>
    /// <exception cref="NetAdminInvalidOperationException">用户不存在</exception>
    public async Task<uint> ResetPasswordAsync(ResetPasswordReq req)
    {
        return !await verifyCodeService.VerifyAsync(req.VerifySmsCodeReq)
            ? throw new NetAdminInvalidOperationException(Ln.验证码不正确)
            : (uint)await Rpo.UpdateDiy
                             .SetSource((await Rpo.Where(a => a.Mobile == req.VerifySmsCodeReq.DestDevice)
                                                  .ToOneAsync(a => new { a.Version, a.Id })).Adapt<Sys_User>() with {
                                            Password = req.PasswordText.Pwd().Guid()
                                        })
                             .UpdateColumns(a => a.Password)
                             .ExecuteAffrowsAsync();
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req)
    {
        if (await Rpo.UpdateDiy
                     .SetSource(req with {
                                             Id = UserToken.Id
                                           , Version = Rpo.Where(a => a.Id == UserToken.Id).ToOne(a => a.Version)
                                         })
                     .UpdateColumns(a => a.Avatar)
                     .ExecuteAffrowsAsync() <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = UserToken.Id } }))
                  .First()
                  .Adapt<UserInfoRsp>();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret));
        return ret;
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetEmailAsync(SetEmailReq req)
    {
        var user = Rpo.Where(a => a.Id == UserToken.Id).ToOne(a => new { a.Mobile, a.Version, a.Email });

        // 如果已绑定手机号、需要手机安全验证
        if (!user.Mobile.NullOrEmpty()) {
            if (!await verifyCodeService.VerifyAsync(req.VerifySmsCodeReq)) {
                throw new NetAdminInvalidOperationException(Ln.验证码不正确);
            }

            if (user.Mobile != req.VerifySmsCodeReq.DestDevice) {
                throw new NetAdminInvalidOperationException($"{Ln.手机号码} {Ln.不正确}");
            }
        }

        if (await Rpo.UpdateDiy
                     .SetSource(new Sys_User { Email = req.DestDevice, Id = UserToken.Id, Version = user.Version })
                     .UpdateColumns(a => a.Email)
                     .ExecuteAffrowsAsync() <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = UserToken.Id } }))
                  .First()
                  .Adapt<UserInfoRsp>();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret));
        return ret;
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetMobileAsync(SetMobileReq req)
    {
        var user = await Rpo.Where(a => a.Id == UserToken.Id).ToOneAsync(a => new { a.Version, a.Mobile });

        if (!user.Mobile.NullOrEmpty()) {
            // 已有手机号，需验证旧手机
            if (!await verifyCodeService.VerifyAsync(req.OriginVerifySmsCodeReq)) {
                throw new NetAdminInvalidOperationException($"{Ln.旧手机号码} {Ln.验证码不正确}");
            }

            if (user.Mobile != req.OriginVerifySmsCodeReq.DestDevice) {
                throw new NetAdminInvalidOperationException($"{Ln.旧手机号码} {Ln.不正确}");
            }
        }

        // 验证新手机号
        if (!await verifyCodeService.VerifyAsync(req.NewVerifySmsCodeReq)) {
            throw new NetAdminInvalidOperationException($"{Ln.新手机号码} {Ln.验证码不正确}");
        }

        if (await Rpo.UpdateDiy
                     .SetSource(new Sys_User {
                                                 Version = user.Version
                                               , Id      = UserToken.Id
                                               , Mobile  = req.NewVerifySmsCodeReq.DestDevice
                                             })
                     .UpdateColumns(a => a.Mobile)
                     .ExecuteAffrowsAsync() <= 0) {
            throw new NetAdminUnexpectedException();
        }

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = UserToken.Id } }))
                  .First()
                  .Adapt<UserInfoRsp>();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret));
        return ret;
    }

    /// <inheritdoc />
    public async Task<uint> SetPasswordAsync(SetPasswordReq req)
    {
        var version = await Rpo.Where(a => a.Id == UserToken.Id && a.Password == req.OldPassword.Pwd().Guid())
                               .ToOneAsync(a => new long?(a.Version));
        if (version != null) {
            var ret = await Rpo.UpdateDiy
                               .SetSource(new Sys_User {
                                                           Id       = UserToken.Id
                                                         , Password = req.NewPassword.Pwd().Guid()
                                                         , Version  = version.Value
                                                       })
                               .UpdateColumns(a => a.Password)
                               .ExecuteAffrowsAsync();
            return ret <= 0 ? throw new NetAdminUnexpectedException() : (uint)ret;
        }

        throw new NetAdminInvalidInputException($"{Ln.旧密码} {Ln.不正确}");
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> UpdateAsync(UpdateUserReq req)
    {
        await CreateUpdateCheckAsync(req);

        // 主表
        var entity     = req.Adapt<Sys_User>();
        var ignoreCols = new List<string> { nameof(Sys_User.Token) };
        if (entity.Password == Guid.Empty) {
            ignoreCols.Add(nameof(Sys_User.Password));
        }

        _ = await Rpo.UpdateDiy.SetSource(entity).IgnoreColumns(ignoreCols.ToArray()).ExecuteAffrowsAsync();

        // 档案表
        _ = await userProfileService.UpdateAsync(req.Profile);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles));

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = req.Id } })).First();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret.Adapt<UserInfoRsp>()));
        return ret;
    }

    /// <inheritdoc />
    public Task UpdateSingleAsync(UpdateUserReq req)
    {
        return Rpo.UpdateAsync(req);
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> UserInfoAsync()
    {
        var dbUser = await Rpo.Where(a => a.Token == UserToken.Token && a.Enabled)
                              .Include(a => a.Dept)
                              .IncludeMany( //
                                  a => a.Roles
                                , then => then.Where(a => a.Enabled)
                                              .IncludeMany(a => a.Menus)
                                              .IncludeMany(a => a.Depts)
                                              .IncludeMany(a => a.Apis))
                              .ToOneAsync();
        return dbUser.Adapt<UserInfoRsp>();
    }

    /// <inheritdoc />
    protected override Task<Sys_User> UpdateForSqliteAsync(Sys_User req)
    {
        throw new NotImplementedException();
    }

    private static LoginRsp LoginInternal(IFieldEnabled dbUser)
    {
        if (!dbUser.Enabled) {
            throw new NetAdminInvalidOperationException(Ln.请联系管理员激活账号);
        }

        var tokenPayload
            = new Dictionary<string, object> { { nameof(ContextUserToken), dbUser.Adapt<ContextUserToken>() } };

        var accessToken = JWTEncryption.Encrypt(tokenPayload);
        return new LoginRsp {
                                AccessToken  = accessToken
                              , RefreshToken = JWTEncryption.GenerateRefreshToken(accessToken)
                            };
    }

    private async Task CreateUpdateCheckAsync(CreateUpdateUserReq req)
    {
        // 检查角色是否存在
        var roles = await Rpo.Orm.Select<Sys_Role>()
                             .ForUpdate()
                             .Where(a => req.RoleIds.Contains(a.Id))
                             .ToListAsync(a => a.Id);
        if (roles.Count != req.RoleIds.Count) {
            throw new NetAdminInvalidOperationException(Ln.角色不存在);
        }

        // 检查部门是否存在
        var dept = await Rpo.Orm.Select<Sys_Dept>().ForUpdate().Where(a => req.DeptId == a.Id).ToListAsync(a => a.Id);

        if (dept.Count != 1) {
            throw new NetAdminInvalidOperationException(Ln.部门不存在);
        }
    }

    private ISelect<Sys_User> QueryInternal(QueryReq<QueryUserReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                     .WhereDynamic(req.Filter)
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);
        if (!req.Prop?.Equals(nameof(req.Filter.Id), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.Id);
        }

        return ret;
    }

    private async Task<ISelect<Sys_User>> QueryInternalAsync(QueryReq<QueryUserReq> req)
    {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Rpo.Orm.Select<Sys_Dept>()
                               .Where(a => a.Id == req.Filter.DeptId)
                               .AsTreeCte()
                               .ToListAsync(a => a.Id);
        }

        var ret = Rpo.Select.Include(a => a.Dept)
                     .IncludeMany(a => a.Roles.Select(b => new Sys_Role { Id = b.Id, Name = b.Name }))
                     .WhereDynamicFilter(req.DynamicFilter)
                     .WhereIf(deptIds != null, a => deptIds.Contains(a.DeptId))
                     .WhereIf( //
                         req.Filter?.Id > 0, a => a.Id == req.Filter.Id)
                     .WhereIf( //
                         req.Filter?.RoleId > 0, a => a.Roles.Any(b => b.Id == req.Filter.RoleId))
                     .WhereIf( //
                         req.Keywords?.Length > 0
                       , a => a.UserName.Contains(req.Keywords) || a.Id == req.Keywords.Int64Try(0) ||
                              a.Mobile.Contains(req.Keywords)   || a.Email.Contains(req.Keywords)   ||
                              a.Summary.Contains(req.Keywords))
                     .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}