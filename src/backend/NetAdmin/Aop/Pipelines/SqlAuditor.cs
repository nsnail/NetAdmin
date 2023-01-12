using System.Reflection;
using FreeSql.Aop;
using Furion;
using Furion.DependencyInjection;
using NetAdmin.Aop.Attributes;
using Yitter.IdGenerator;

namespace NetAdmin.Aop.Pipelines;

/// <summary>
///     Sql审核器
/// </summary>
public class SqlAuditor : ISingleton
{
    private readonly ILogger<SqlAuditor> _logger;

    /// <summary>
    ///     数据库服务器时钟偏移
    /// </summary>
    private readonly TimeSpan _timeOffset;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SqlAuditor" /> class.
    /// </summary>
    public SqlAuditor(ILogger<SqlAuditor> logger)
    {
        _logger = logger;

        // 设置服务器时间偏差
        _timeOffset = DateTime.UtcNow.Subtract(
            App.GetRequiredService<IFreeSql>().Ado.QuerySingle(() => DateTime.UtcNow));

        _logger.LogInformation("{}", $"{nameof(SqlAuditor)} 初始化完毕");
    }

    /// <summary>
    ///     对Insert/Update的数据加工
    /// </summary>
    public void DataAuditHandler(object sender, AuditValueEventArgs e)
    {
        // 设置服务器时间字段
        if (e.Property.GetCustomAttribute<ServerTimeAttribute>(false) is { Enable: true }   &&
            (e.Column.CsType == typeof(DateTime) || e.Column.CsType   == typeof(DateTime?)) &&
            (e.Value         == null             || (DateTime)e.Value == default || (DateTime?)e.Value == default)) {
            e.Value = DateTime.Now.Subtract(_timeOffset);
        }

        // 设置雪花id字段
        if (e.Column.CsType == typeof(long)                                              &&
            e.Property.GetCustomAttribute<SnowflakeAttribute>(false) is { Enable: true } &&
            (e.Value == null || (long)e.Value == default || (long?)e.Value == default)) {
            e.Value = YitIdHelper.NextId();
        }
    }
}