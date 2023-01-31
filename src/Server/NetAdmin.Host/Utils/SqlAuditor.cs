using System.Reflection;
using FreeSql.Aop;
using Furion;
using Furion.DependencyInjection;
using NetAdmin.Domain.Contexts;
using NSExt.Extensions;
using Yitter.IdGenerator;

namespace NetAdmin.Host.Utils;

/// <summary>
///     Sql审核器
/// </summary>
public class SqlAuditor : ISingleton
{
    private static readonly string[] _auditColumns = {
                                                         nameof(IFieldAdd.CreatedUserName)
                                                       , nameof(IFieldAdd.CreatedUserId)
                                                       , nameof(IFieldUpdate.ModifiedUserName)
                                                       , nameof(IFieldUpdate.ModifiedUserId)
                                                     };

    /// <summary>
    ///     数据库服务器时钟偏移
    /// </summary>
    private readonly TimeSpan _timeOffset;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlAuditor" /> class.
    /// </summary>
    public SqlAuditor(ILogger<SqlAuditor> logger)
    {
        // 设置服务器时间偏差
        _timeOffset = DateTime.UtcNow.Subtract(
            App.GetRequiredService<IFreeSql>().Ado.QuerySingle(() => DateTime.UtcNow));

        logger.Info($"{Ln.Database_server_clock_offset} {_timeOffset}");
    }

    /// <summary>
    ///     对Insert/Update的数据加工
    /// </summary>
    public void DataAuditHandler(object sender, AuditValueEventArgs e)
    {
        SetServerTime(e);
        SetSnowflake(e);

        if (!_auditColumns.Contains(e.Property.Name)) {
            return;
        }

        var user = App.GetService<ContextUser>();
        if (user is null || user.Id == 0) {
            return;
        }

        switch (e.AuditValueType) {
            case AuditValueType.Insert:
                SetCreator(e, user);
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
    ///     设置创建人
    /// </summary>
    private static void SetCreator(AuditValueEventArgs e, ContextUser user)
    {
        switch (e.Property.Name) {
            case nameof(IFieldAdd.CreatedUserId):
                if (e.Value is null or long and 0) {
                    e.Value = user.Id;
                }

                break;

            case nameof(IFieldAdd.CreatedUserName):
                if (e.Value is null or "") {
                    e.Value = user.UserName;
                }

                break;
        }
    }

    /// <summary>
    ///     设置雪花id字段
    /// </summary>
    private static void SetSnowflake(AuditValueEventArgs e)
    {
        var isSnowflake = e.Property.GetCustomAttribute<SnowflakeAttribute>(false) is { Enable: true };
        var isLongType  = e.Column.CsType == typeof(long);
        var isNoValue   = e.Value is null or long and 0;
        if (isSnowflake && isLongType && isNoValue) {
            e.Value = YitIdHelper.NextId();
        }
    }

    /// <summary>
    ///     设置更新人
    /// </summary>
    private static void SetUpdater(AuditValueEventArgs e, ContextUser user)
    {
        e.Value = e.Property.Name switch {
                      nameof(IFieldUpdate.ModifiedUserId)   => user.Id
                    , nameof(IFieldUpdate.ModifiedUserName) => user.UserName
                    , _                                     => e.Value
                  };
    }

    /// <summary>
    ///     设置服务器时间字段
    /// </summary>
    private void SetServerTime(AuditValueEventArgs e)
    {
        var isServerTime = e.Property.GetCustomAttribute<ServerTimeAttribute>(false) is { Enable: true };
        var isDateType   = e.Column.CsType == typeof(DateTime) || e.Column.CsType == typeof(DateTime?);
        var isNoValue    = e.Value is null                     || (e.Value is DateTime val && val == default);
        if (isServerTime && isDateType && isNoValue) {
            e.Value = DateTime.Now.Subtract(_timeOffset);
        }
    }
}