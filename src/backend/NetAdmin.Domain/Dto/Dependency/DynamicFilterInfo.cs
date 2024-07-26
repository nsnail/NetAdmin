using NetAdmin.Domain.Enums;

namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     动态过滤条件
/// </summary>
public sealed record DynamicFilterInfo : DataAbstraction
{
    /// <summary>
    ///     字段名
    /// </summary>
    public string Field { get; init; }

    /// <summary>
    ///     子过滤条件
    /// </summary>
    public List<DynamicFilterInfo> Filters { get; init; }

    /// <summary>
    ///     子过滤条件逻辑关系
    /// </summary>
    public DynamicFilterLogics Logic { get; init; }

    /// <summary>
    ///     操作符
    /// </summary>
    public DynamicFilterOperators Operator { get; init; }

    /// <summary>
    ///     值
    /// </summary>
    public object Value { get; init; }

    /// <summary>
    ///     隐式转换为 FreeSql 的 DynamicFilterInfo 对象
    /// </summary>
    public static implicit operator FreeSql.Internal.Model.DynamicFilterInfo(DynamicFilterInfo d)
    {
        var ret = d.Adapt<FreeSql.Internal.Model.DynamicFilterInfo>();
        ProcessDynamicFilter(ret);
        return ret;
    }

    private static void ProcessDynamicFilter(FreeSql.Internal.Model.DynamicFilterInfo d)
    {
        if (d?.Filters != null) {
            foreach (var filterInfo in d.Filters) {
                ProcessDynamicFilter(filterInfo);
            }
        }

        if (new[] { nameof(IFieldCreatedClientIp.CreatedClientIp), nameof(IFieldModifiedClientIp.ModifiedClientIp) }
            .Contains(d?.Field, StringComparer.OrdinalIgnoreCase)) {
            var val = d!.Value?.ToString();
            if (val?.IsIpV4() == true) {
                d.Value = val.IpV4ToInt32();
            }
        }
        else if (d?.Operator == DynamicFilterOperator.DateRange) {
            var values = ((JsonElement)d.Value).Deserialize<string[]>();
            if (!DateTime.TryParse(values[0], CultureInfo.InvariantCulture, out _)) {
                var result = values[0]
                             .ExecuteCSharpCodeAsync<DateTime>([typeof(DateTime).Assembly], nameof(System))
                             .ConfigureAwait(false)
                             .GetAwaiter()
                             .GetResult();
                values[0] = $"{result:yyyy-MM-dd HH:mm:ss}";
            }

            if (!DateTime.TryParse(values[1], CultureInfo.InvariantCulture, out _)) {
                var result = values[1]
                             .ExecuteCSharpCodeAsync<DateTime>([typeof(DateTime).Assembly], nameof(System))
                             .ConfigureAwait(false)
                             .GetAwaiter()
                             .GetResult();
                values[1] = $"{result:yyyy-MM-dd HH:mm:ss}";
            }

            d.Value = values;
        }
    }
}