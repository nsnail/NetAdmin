using NetAdmin.Domain.Enums;

namespace NetAdmin.Domain.Dto;

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
        #pragma warning disable VSTHRD002
        ProcessDynamicFilterAsync(ret).ConfigureAwait(false).GetAwaiter().GetResult();
        #pragma warning restore VSTHRD002
        return ret;
    }

    /// <summary>
    ///     添加子过滤条件
    /// </summary>
    public DynamicFilterInfo Add(DynamicFilterInfo df)
    {
        if (Filters == null) {
            return this with { Filters = [df] };
        }

        Filters.Add(df);
        return this;
    }

    /// <summary>
    ///     添加过滤条件
    /// </summary>
    public DynamicFilterInfo Add(string field, DynamicFilterOperators opt, object val)
    {
        return Add(new DynamicFilterInfo { Field = field, Operator = opt, Value = val });
    }

    /// <summary>
    ///     添加过滤条件
    /// </summary>
    public DynamicFilterInfo AddIf(bool condition, string field, DynamicFilterOperators opt, object val)
    {
        return !condition ? this : Add(field, opt, val);
    }

    /// <summary>
    ///     添加过滤条件
    /// </summary>
    public DynamicFilterInfo AddIf(bool condition, DynamicFilterInfo df)
    {
        return !condition ? this : Add(df);
    }

    private static async Task ParseDateExpAsync(FreeSql.Internal.Model.DynamicFilterInfo d)
    {
        var values = ((JsonElement)d.Value).Deserialize<string[]>();
        if (!DateTime.TryParse(values[0], CultureInfo.InvariantCulture, out _)) {
            var result = await values[0].ExecuteCSharpCodeAsync<DateTime>([typeof(DateTime).Assembly], nameof(System)).ConfigureAwait(false);
            values[0] = $"{result:yyyy-MM-dd HH:mm:ss}";
        }

        if (!DateTime.TryParse(values[1], CultureInfo.InvariantCulture, out _)) {
            var result = await values[1].ExecuteCSharpCodeAsync<DateTime>([typeof(DateTime).Assembly], nameof(System)).ConfigureAwait(false);
            values[1] = $"{result:yyyy-MM-dd HH:mm:ss}";
        }

        d.Value = values;
    }

    private static async Task ProcessDynamicFilterAsync(FreeSql.Internal.Model.DynamicFilterInfo d)
    {
        if (d?.Filters != null) {
            foreach (var filterInfo in d.Filters) {
                await ProcessDynamicFilterAsync(filterInfo).ConfigureAwait(false);
            }
        }

        if (new[] { nameof(IFieldCreatedClientIp.CreatedClientIp), nameof(IFieldModifiedClientIp.ModifiedClientIp) }.Contains(
                d?.Field, StringComparer.OrdinalIgnoreCase)) {
            var val = d!.Value?.ToString();
            if (val?.IsIpV4() == true) {
                d.Value = val.IpV4ToInt32();
            }
        }
        else if (d?.Operator == DynamicFilterOperator.DateRange) {
            await ParseDateExpAsync(d).ConfigureAwait(false);
        }
    }
}