using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IUserService" />
public sealed class UserService(
    BasicRepository<Sys_User, long> rpo                //
  , IUserProfileService             userProfileService //
  , IVerifyCodeService              verifyCodeService  //
  , IEventPublisher                 eventPublisher)    //
    : RepositoryService<Sys_User, long, IUserService>(rpo), IUserService
{
    private readonly Expression<Func<Sys_User, Sys_User>> _listUserExp = a => new Sys_User {
                                                                                               Id            = a.Id
                                                                                             , Avatar        = a.Avatar
                                                                                             , Email         = a.Email
                                                                                             , Mobile        = a.Mobile
                                                                                             , Enabled       = a.Enabled
                                                                                             , UserName      = a.UserName
                                                                                             , Summary       = a.Summary
                                                                                             , Version       = a.Version
                                                                                             , CreatedTime   = a.CreatedTime
                                                                                             , LastLoginTime = a.LastLoginTime
                                                                                             , Dept = new Sys_Dept {
                                                                                                   Id = a.Dept.Id, Name = a.Dept.Name
                                                                                               }
                                                                                             , Roles           = a.Roles
                                                                                             , CreatedUserId   = a.CreatedUserId
                                                                                             , CreatedUserName = a.CreatedUserName
                                                                                           };

    /// <inheritdoc />
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        var ret = 0;

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var item in req.Items) {
            ret += await DeleteAsync(item).ConfigureAwait(false);
        }

        return ret;
    }

    /// <inheritdoc />
    public async Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        req.ThrowIfInvalid();
        return !await Rpo.Select.Where(a => a.Mobile == req.Mobile && a.Id != req.Id).AnyAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        req.ThrowIfInvalid();
        return !await Rpo.Select.Where(a => a.UserName == req.UserName && a.Id != req.Id).AnyAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryUserReq> req)
    {
        req.ThrowIfInvalid();
        #pragma warning disable VSTHRD103
        return QueryInternal(req).WithNoLockNoWait().CountAsync();
        #pragma warning restore VSTHRD103
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        req.ThrowIfInvalid();
        var roles = await CreateEditCheckAsync(req).ConfigureAwait(false);

        // 主表
        var entity = req.Adapt<Sys_User>();
        var dbUser = await Rpo.InsertAsync(entity).ConfigureAwait(false);

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles)).ConfigureAwait(false);

        // 档案表
        var appConfig = UserProfileService.BuildAppConfig(roles);

        _ = await userProfileService.CreateAsync((req.Profile ?? new CreateUserProfileReq()) with //
                                                 {
                                                     Id = dbUser.Id //
                                                   , AppConfig = appConfig
                                                 })
                                    .ConfigureAwait(false);
        var userList = await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = dbUser.Id } }).ConfigureAwait(false);

        // 发布用户创建事件
        var ret = userList.First();
        await eventPublisher.PublishAsync(new UserCreatedEvent(ret.Adapt<UserInfoRsp>())).ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public async Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();

        // 删除主表
        var ret = await Rpo.DeleteAsync(req.Id).ConfigureAwait(false);

        // 删除分表
        _ = await Rpo.Orm.Delete<Sys_UserRole>(new { UserId = req.Id }).ExecuteAffrowsAsync().ConfigureAwait(false);

        // 删除档案表
        _ = await userProfileService.DeleteAsync(req).ConfigureAwait(false);

        return ret;
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> EditAsync(EditUserReq req)
    {
        req.ThrowIfInvalid();
        _ = await CreateEditCheckAsync(req).ConfigureAwait(false);

        // 主表
        var entity     = req.Adapt<Sys_User>();
        var ignoreCols = new List<string> { nameof(Sys_User.Id), nameof(Sys_User.Token) };
        if (entity.Password == Guid.Empty) {
            ignoreCols.Add(nameof(Sys_User.Password));
        }

        _ = await UpdateAsync(entity, null, ignoreCols.ToArray()).ConfigureAwait(false);

        // 档案表
        if (req.Profile != null) {
            _ = await userProfileService.EditAsync(req.Profile).ConfigureAwait(false);
        }

        // 分表
        await Rpo.SaveManyAsync(entity, nameof(entity.Roles)).ConfigureAwait(false);

        var ret = await GetAsync(new QueryUserReq { Id = req.Id }).ConfigureAwait(false);

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret.Adapt<UserInfoRsp>())).ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public async Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        req.ThrowIfInvalid();
        return await (await QueryInternalAsync(req).ConfigureAwait(false)).AnyAsync().ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserReq> req)
    {
        req.ThrowIfInvalid();
        return ExportAsync<QueryUserReq, ExportUserRsp>(QueryInternal, req, Ln.用户导出);
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        req.ThrowIfInvalid();
        var ret = await (await QueryInternalAsync(new QueryReq<QueryUserReq> { Filter = req, Order = Orders.None }).ConfigureAwait(false))
                        .ToOneAsync()
                        .ConfigureAwait(false);
        return ret.Adapt<QueryUserRsp>();
    }

    /// <inheritdoc />
    public Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync()
    {
        return userProfileService.GetSessionUserAppConfigAsync();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">用户名或密码错误</exception>
    public async Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        req.ThrowIfInvalid();
        var pwd = req.Password.Pwd().Guid();

        Sys_User dbUser;
        #pragma warning disable IDE0045
        if (new MobileAttribute().IsValid(req.Account)) {
            #pragma warning restore IDE0045
            dbUser = await Rpo.Where(a => a.Mobile == req.Account && a.Password == pwd).ToOneAsync().ConfigureAwait(false);
        }
        else {
            dbUser = new EmailAddressAttribute().IsValid(req.Account)
                ? await Rpo.Where(a => a.Email    == req.Account && a.Password == pwd).ToOneAsync().ConfigureAwait(false)
                : await Rpo.Where(a => a.UserName == req.Account && a.Password == pwd).ToOneAsync().ConfigureAwait(false);
        }

        return dbUser == null ? throw new NetAdminInvalidOperationException(Ln.用户名或密码错误) : await LoginInternalAsync(dbUser).ConfigureAwait(false);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">验证码不正确</exception>
    /// <exception cref="NetAdminInvalidOperationException">用户不存在</exception>
    public async Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        req.ThrowIfInvalid();
        if (!await verifyCodeService.VerifyAsync(req.Adapt<VerifySmsCodeReq>()).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.验证码不正确);
        }

        var dbUser = await Rpo.Where(a => a.Mobile == req.DestDevice).ToOneAsync().ConfigureAwait(false);
        return dbUser == null ? throw new NetAdminInvalidOperationException(Ln.用户不存在) : await LoginInternalAsync(dbUser).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<LoginRsp> LoginByUserIdAsync(long userId)
    {
        var dbUser = await Rpo.Where(a => a.Id == userId).ToOneAsync().ConfigureAwait(false);

        return await LoginInternalAsync(dbUser).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        req.ThrowIfInvalid();
        var listUserExp  = req.GetToListExp<Sys_User>() ?? _listUserExp;
        var includeRoles = listUserExp == _listUserExp;
        var select       = await QueryInternalAsync(req, includeRoles).ConfigureAwait(false);
        IEnumerable<Sys_User> list = await select.Page(req.Page, req.PageSize)
                                                 .WithNoLockNoWait()
                                                 .Count(out var total)
                                                 .ToListAsync(listUserExp)
                                                 .ConfigureAwait(false);
        if (includeRoles) {
            list = list.Select(x => x with { Roles = x.Roles.OrderBy(y => y.Sort).ThenBy(y => y.Id).ToList() });
        }

        return new PagedQueryRsp<QueryUserRsp>(req.Page, req.PageSize, total, list.Adapt<IEnumerable<QueryUserRsp>>());
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        req.ThrowIfInvalid();
        var list = await (await QueryInternalAsync(req, false).ConfigureAwait(false)).WithNoLockNoWait()
                                                                                     .Take(req.Count)
                                                                                     .ToListAsync(a => new Sys_User {
                                                                                                      Id = a.Id, UserName = a.UserName
                                                                                                  })
                                                                                     .ConfigureAwait(false);
        return list.Adapt<IEnumerable<QueryUserRsp>>();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        req.ThrowIfInvalid();
        return userProfileService.QueryAsync(req);
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">验证码不正确</exception>
    public async Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
    {
        req.ThrowIfInvalid();
        if (!await verifyCodeService.VerifyAsync(req.VerifySmsCodeReq).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.验证码不正确);
        }

        var createReq = req.Adapt<CreateUserReq>() with { Profile = new CreateUserProfileReq() };
        return (await CreateAsync(createReq).ConfigureAwait(false)).Adapt<UserInfoRsp>();
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">验证码不正确</exception>
    /// <exception cref="NetAdminInvalidOperationException">用户不存在</exception>
    public async Task<int> ResetPasswordAsync(ResetPasswordReq req)
    {
        req.ThrowIfInvalid();
        if (!await verifyCodeService.VerifyAsync(req.VerifySmsCodeReq).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException(Ln.验证码不正确);
        }

        var dto = (await Rpo.Where(a => a.Mobile == req.VerifySmsCodeReq.DestDevice).ToOneAsync(a => new { a.Version, a.Id }).ConfigureAwait(false))
                  .Adapt<Sys_User>() with {
                                              Password = req.PasswordText.Pwd().Guid()
                                          };
        return await UpdateAsync(dto, [nameof(Sys_User.Password)]).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req)
    {
        req.ThrowIfInvalid();
        if (await UpdateAsync(
                    req with {
                                 Id = UserToken.Id
                               , Version = await Rpo.Where(a => a.Id == UserToken.Id).ToOneAsync(a => a.Version).ConfigureAwait(false)
                             }
                  , [nameof(Sys_User.Avatar)])
                .ConfigureAwait(false) <= 0) {
            return null;
        }

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = UserToken.Id } }).ConfigureAwait(false)).First()
            .Adapt<UserInfoRsp>();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret)).ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetEmailAsync(SetEmailReq req)
    {
        req.ThrowIfInvalid();
        var user = await Rpo.Where(a => a.Id == UserToken.Id).ToOneAsync(a => new { a.Mobile, a.Version, a.Email }).ConfigureAwait(false);

        // 如果已绑定手机号码、需要手机安全验证
        if (!user.Mobile.NullOrEmpty()) {
            if (!await verifyCodeService.VerifyAsync(req.VerifySmsCodeReq).ConfigureAwait(false)) {
                throw new NetAdminInvalidOperationException(Ln.验证码不正确);
            }

            if (user.Mobile != req.VerifySmsCodeReq.DestDevice) {
                throw new NetAdminInvalidOperationException($"{Ln.手机号码不正确}");
            }
        }

        if (await UpdateAsync( //
                    new Sys_User { Email = req.DestDevice, Id = UserToken.Id, Version = user.Version }, [nameof(Sys_User.Email)])
                .ConfigureAwait(false) <= 0) {
            return null;
        }

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = UserToken.Id } }).ConfigureAwait(false)).First()
            .Adapt<UserInfoRsp>();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret)).ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public Task<int> SetEnabledAsync(SetUserEnabledReq req)
    {
        req.ThrowIfInvalid();
        return UpdateAsync(req, [nameof(req.Enabled)]);
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> SetMobileAsync(SetMobileReq req)
    {
        req.ThrowIfInvalid();
        var user = await Rpo.Where(a => a.Id == UserToken.Id).ToOneAsync(a => new { a.Version, a.Mobile }).ConfigureAwait(false);

        if (!user.Mobile.NullOrEmpty()) {
            // 已有手机号码，需验证旧手机
            if (!await verifyCodeService.VerifyAsync(req.OriginVerifySmsCodeReq).ConfigureAwait(false)) {
                throw new NetAdminInvalidOperationException($"{Ln.旧手机号码验证码不正确}");
            }

            if (user.Mobile != req.OriginVerifySmsCodeReq.DestDevice) {
                throw new NetAdminInvalidOperationException($"{Ln.旧手机号码不正确}");
            }
        }

        // 验证新手机号码
        if (!await verifyCodeService.VerifyAsync(req.NewVerifySmsCodeReq).ConfigureAwait(false)) {
            throw new NetAdminInvalidOperationException($"{Ln.新手机号码验证码不正确}");
        }

        if (await UpdateAsync( //
                    new Sys_User { Version = user.Version, Id = UserToken.Id, Mobile = req.NewVerifySmsCodeReq.DestDevice }
                  , [nameof(Sys_User.Mobile)])
                .ConfigureAwait(false) <= 0) {
            return null;
        }

        var ret = (await QueryAsync(new QueryReq<QueryUserReq> { Filter = new QueryUserReq { Id = UserToken.Id } }).ConfigureAwait(false)).First()
            .Adapt<UserInfoRsp>();

        // 发布用户更新事件
        await eventPublisher.PublishAsync(new UserUpdatedEvent(ret)).ConfigureAwait(false);
        return ret;
    }

    /// <inheritdoc />
    public async Task<int> SetPasswordAsync(SetPasswordReq req)
    {
        req.ThrowIfInvalid();
        var version = await Rpo.Where(a => a.Id == UserToken.Id && a.Password == req.OldPassword.Pwd().Guid())
                               .ToOneAsync(a => new long?(a.Version))
                               .ConfigureAwait(false) ?? throw new NetAdminInvalidOperationException($"{Ln.旧密码不正确}");

        return await UpdateAsync( //
                new Sys_User { Id = UserToken.Id, Password = req.NewPassword.Pwd().Guid(), Version = version }, [nameof(Sys_User.Password)])
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req)
    {
        return userProfileService.SetSessionUserAppConfigAsync(req);
    }

    /// <inheritdoc />
    public async Task<UserInfoRsp> UserInfoAsync()
    {
        var dbUser = await Rpo.Where(a => a.Token == UserToken.Token && a.Enabled)
                              .WithNoLockNoWait()
                              .Include(a => a.Dept)
                              .IncludeMany(a => a.Roles, OtherIncludes)
                              .ToOneAsync()
                              .ConfigureAwait(false);
        return dbUser.Adapt<UserInfoRsp>();

        static void OtherIncludes(ISelect<Sys_Role> select)
        {
            _ = select.Where(a => a.Enabled)
                      .WithNoLockNoWait()
                      .IncludeMany(a => a.Menus, then => then.WithNoLockNoWait())
                      .IncludeMany(a => a.Depts, then => then.WithNoLockNoWait())
                      .IncludeMany(a => a.Apis,  then => then.WithNoLockNoWait());
        }
    }

    private async Task<Dictionary<long, string>> CreateEditCheckAsync(CreateEditUserReq req)
    {
        // 检查角色是否存在
        var roles = await Rpo.Orm.Select<Sys_Role>()
                             .WithNoLockNoWait()
                             .Where(a => req.RoleIds.Contains(a.Id))
                             .ToDictionaryAsync(a => a.Id, a => a.DashboardLayout)
                             .ConfigureAwait(false);
        if (roles.Count != req.RoleIds.Count) {
            throw new NetAdminInvalidOperationException(Ln.角色不存在);
        }

        // 检查部门是否存在
        var dept = await Rpo.Orm.Select<Sys_Dept>().WithNoLockNoWait().Where(a => req.DeptId == a.Id).ToListAsync(a => a.Id).ConfigureAwait(false);

        return dept.Count != 1 ? throw new NetAdminInvalidOperationException(Ln.部门不存在) : roles;
    }

    private async Task<LoginRsp> LoginInternalAsync(Sys_User dbUser)
    {
        if (!dbUser.Enabled) {
            throw new NetAdminInvalidOperationException(Ln.请联系管理员激活账号);
        }

        _ = await UpdateAsync(dbUser with { LastLoginTime = DateTime.Now }, [nameof(Sys_User.LastLoginTime)], ignoreVersion: true)
            .ConfigureAwait(false);

        var tokenPayload = new Dictionary<string, object> { { nameof(ContextUserToken), dbUser.Adapt<ContextUserToken>() } };

        var accessToken = JWTEncryption.Encrypt(tokenPayload);
        return new LoginRsp { AccessToken = accessToken, RefreshToken = JWTEncryption.GenerateRefreshToken(accessToken) };
    }

    private ISelect<Sys_User> QueryInternal(QueryReq<QueryUserReq> req, IEnumerable<long> deptIds, bool includeRoles = true)
    {
        var ret = Rpo.Select.Include(a => a.Dept);

        #pragma warning disable RCS1196

        // ReSharper disable InvokeAsExtensionMethod
        if (includeRoles) {
            ret = ret.IncludeMany(a => Enumerable.Select(a.Roles, b => new Sys_Role { Id = b.Id, Name = b.Name }));
        }

        ret = ret.WhereDynamicFilter(req.DynamicFilter)
                 .WhereIf(deptIds != null, a => deptIds.Contains(a.DeptId))
                 .WhereIf( //
                     req.Filter?.Id > 0, a => a.Id == req.Filter.Id)
                 .WhereIf( //
                     req.Filter?.RoleId > 0, a => Enumerable.Any(a.Roles, b => b.Id == req.Filter.RoleId))
                 .WhereIf( //
                     req.Keywords?.Length > 0
                   , a => a.Id == req.Keywords.Int64Try(0) || a.UserName == req.Keywords || a.Mobile == req.Keywords || a.Email == req.Keywords ||
                          a.Summary.Contains(req.Keywords));

        // ReSharper restore InvokeAsExtensionMethod
        #pragma warning restore RCS1196

        // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }

    private ISelect<Sys_User> QueryInternal(QueryReq<QueryUserReq> req)
    {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = Rpo.Orm.Select<Sys_Dept>().Where(a => a.Id == req.Filter.DeptId).AsTreeCte().ToList(a => a.Id);
        }

        return QueryInternal(req, deptIds);
    }

    private async Task<ISelect<Sys_User>> QueryInternalAsync(QueryReq<QueryUserReq> req, bool includeRoles = true)
    {
        IEnumerable<long> deptIds = null;
        if (req.Filter?.DeptId > 0) {
            deptIds = await Rpo.Orm.Select<Sys_Dept>().Where(a => a.Id == req.Filter.DeptId).AsTreeCte().ToListAsync(a => a.Id).ConfigureAwait(false);
        }

        return QueryInternal(req, deptIds, includeRoles);
    }
}