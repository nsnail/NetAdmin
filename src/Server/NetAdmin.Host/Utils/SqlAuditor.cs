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
        _timeOffset = DateTime.UtcNow.Subtract(App.GetService<IFreeSql>().Ado.QuerySingle(() => DateTime.UtcNow));

        logger.Info($"{Ln.Database_server_clock_offset} {_timeOffset}");
    }

    /// <summary>
    ///     对Insert/Update的数据加工
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">e</exception>
    public void DataAuditHandler(object sender, AuditValueEventArgs e)
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
                SetUpdater(e, user);
                break;
            case AuditValueType.InsertOrUpdate:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(e));
        }
    }

    /// <summary>
    ///     设置创建者
    /// </summary>
    private static void SetCreator(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        switch (e.Property.Name) {
            case nameof(IFieldCreatedTime.CreatedTime):
                if (e.Value is null || (e.Value is DateTime val && val == default)) {
                    e.Value = DateTime.Now;
                }

                break;
            case nameof(IFieldCreatedUser.CreatedUserId):
                if (userInfo is not null && e.Value is null or (long and 0)) {
                    e.Value = userInfo.Id;
                }

                break;

            case nameof(IFieldCreatedUser.CreatedUserName):
                if (userInfo is not null && e.Value is null or "") {
                    e.Value = userInfo.UserName;
                }

                break;
            default:
                return;
        }
    }

    /// <summary>
    ///     设置拥有者
    /// </summary>
    private static void SetOwner(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        switch (e.Property.Name) {
            case nameof(IFieldOwner.OwnerId):
                if (userInfo is not null && e.Value is null or (long and 0)) {
                    e.Value = userInfo.Id;
                }

                break;
            case nameof(IFieldOwner.OwnerDeptId):
                if (userInfo is not null && e.Value is null or "") {
                    e.Value = userInfo.DeptId;
                }

                break;
            default:
                return;
        }
    }

    /// <summary>
    ///     设置雪花id字段
    /// </summary>
    private static void SetSnowflake(AuditValueEventArgs e)
    {
        var isSnowflake = e.Property.GetCustomAttribute<SnowflakeAttribute>(false) is not null;
        var isLongType  = e.Column.CsType == typeof(long);
        var isNoValue   = e.Value is null or (long and 0);
        if (isSnowflake && isLongType && isNoValue) {
            e.Value = YitIdHelper.NextId();
        }
    }

    /// <summary>
    ///     设置更新人
    /// </summary>
    private static void SetUpdater(AuditValueEventArgs e, ContextUserInfo userInfo)
    {
        switch (e.Property.Name) {
            case nameof(IFieldModifiedTime.ModifiedTime):
                e.Value = DateTime.Now;
                break;
            case nameof(IFieldModifiedUser.ModifiedUserId):
                if (userInfo is not null && e.Value is null or (long and 0)) {
                    e.Value = userInfo.Id;
                }

                break;

            case nameof(IFieldModifiedUser.ModifiedUserName):
                if (userInfo is not null && e.Value is null or "") {
                    e.Value = userInfo.UserName;
                }

                break;
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