using NetAdmin.Domain.Attributes;
using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using Yitter.IdGenerator;

namespace NetAdmin.Host.Utils;

/// <summary>
///     Sql审核器
/// </summary>
public sealed class SqlAuditor : ISingleton
{
    /// <summary>
    ///     数据库服务器时钟偏移
    /// </summary>
    #pragma warning disable S1450

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly TimeSpan _timeOffset;
    #pragma warning restore S1450

    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlAuditor" /> class.
    /// </summary>
    public SqlAuditor(ILogger<SqlAuditor> logger)
    {
        // 设置服务器时间偏差
        _timeOffset = DateTime.Now.Subtract(App.GetService<IFreeSql>().Ado.QuerySingle(() => DateTime.Now));

        logger.Info($"{Ln.数据库服务器时钟偏移} {_timeOffset}");
    }

    /// <summary>
    ///     对Insert/Update的数据加工
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">e</exception>
    public static void DataAuditHandler(object sender, AuditValueEventArgs e)
    {
        // SetServerTime(e);
        SetSnowflake(e);

        // 设置创建者、修改者信息
        var user = App.GetService<ContextUserInfo>();
        switch (e.AuditValueType) {
            case AuditValueType.Insert:
                SetCreator(e, user);
                SetOwner(e, user);
                break;
            case AuditValueType.Update:
                SetModificator(e, user);
                break;
            case AuditValueType.InsertOrUpdate:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(e));
        }
    }

    private static void SetCreatedClientIp(AuditValueEventArgs e)
    {
        if (e.Value is null or 0) {
            e.Value = App.HttpContext?.GetRealIpAddress()?.MapToIPv4().ToString().IpV4ToInt32();
        }
    }

    private static void SetCreatedReferer(AuditValueEventArgs e)
    {
        if (e.Value is null or "") {
            e.Value = App.HttpContext?.Request.GetRefererUrlAddress();
        }
    }

    private static void SetCreatedTime(AuditValueEventArgs e)
    {
        if (e.Value == null || (e.Value is DateTime val && val == default)) {
            e.Value = DateTime.Now;
        }
    }

    private static void SetCreatedUserAgent(AuditValueEventArgs e)
    {
        if (e.Value is null or "") {
            e.Value = App.HttpContext?.Request.Headers[Chars.FLG_HTTP_HEADER_KEY_USER_AGENT].ToString();
        }
    }

    private static void SetCreatedUserId(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        if (userInfo != null && e.Value is null or (long and 0)) {
            e.Value = userInfo.Id;
        }
    }

    private static void SetCreatedUserName(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        if (userInfo != null && e.Value is null or "") {
            e.Value = userInfo.UserName;
        }
    }

    /// <summary>
    ///     设置创建者
    /// </summary>
    private static void SetCreator(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        switch (e.Property.Name) {
            case nameof(IFieldCreatedTime.CreatedTime):
                SetCreatedTime(e);
                break;
            case nameof(IFieldCreatedUser.CreatedUserId):
                SetCreatedUserId(e, userInfo);
                break;
            case nameof(IFieldCreatedUser.CreatedUserName):
                SetCreatedUserName(e, userInfo);
                break;
            case nameof(IFieldCreatedClient.CreatedClientIp):
                SetCreatedClientIp(e);
                break;
            case nameof(IFieldCreatedClient.CreatedUserAgent):
                SetCreatedUserAgent(e);
                break;
            case nameof(IFieldCreatedClient.CreatedReferer):
                SetCreatedReferer(e);
                break;
            default:
                return;
        }
    }

    /// <summary>
    ///     设置更新人
    /// </summary>
    private static void SetModificator(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        switch (e.Property.Name) {
            // case nameof(IFieldModifiedTime.ModifiedTime):
            //     e.Value = DateTime.Now;
            //     break;
            case nameof(IFieldModifiedUser.ModifiedUserId):
                if (userInfo != null && e.Value is null or (long and 0)) {
                    e.Value = userInfo.Id;
                }

                break;

            case nameof(IFieldModifiedUser.ModifiedUserName):
                if (userInfo != null && e.Value is null or "") {
                    e.Value = userInfo.UserName;
                }

                break;
        }
    }

    /// <summary>
    ///     设置拥有者
    /// </summary>
    private static void SetOwner(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        switch (e.Property.Name) {
            case nameof(IFieldOwner.OwnerId):
                if (userInfo != null && e.Value is null or (long and 0)) {
                    e.Value = userInfo.Id;
                }

                break;
            case nameof(IFieldOwner.OwnerDeptId):
                if (userInfo != null && e.Value is null or "") {
                    e.Value = userInfo.Dept.Id;
                }

                break;
            default:
                return;
        }
    }

    /// <summary>
    ///     设置雪花编号字段
    /// </summary>
    private static void SetSnowflake(AuditValueEventArgs e)
    {
        var isSnowflake = e.Property.GetCustomAttribute<SnowflakeAttribute>(false) != null;
        var isLongType  = e.Column.CsType                                          == typeof(long);
        var isNoValue   = e.Value is null or (long and 0);
        if (isSnowflake && isLongType && isNoValue) {
            e.Value = YitIdHelper.NextId();
        }
    }

    // /// <summary>
    // ///     设置服务器时间字段
    // /// </summary>
    // private void SetServerTime(AuditValueEventArgs e)
    //     #pragma warning restore RCS1213, IDE0051
    // {
    //     var isServerTime = e.Property.GetCustomAttribute<ServerTimeAttribute>(false) is { Enable: true };
    //     var isDateType   = e.Column.CsType                == typeof(DateTime) || e.Column.CsType == typeof(DateTime?);
    //     var hasValue     = e.Value is DateTime val && val != default;
    //     if (isServerTime && isDateType && hasValue) {
    //         e.Value = ((DateTime)e.Value).Subtract(_timeOffset);
    //     }
    // }
}