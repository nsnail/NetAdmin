namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     请求：查询
/// </summary>
public record QueryReq<T> : DataAbstraction
    where T : DataAbstraction, new()
{
    /// <summary>
    ///     取前n条
    /// </summary>
    [Range(1, Numbers.MAX_LIMIT_QUERY)]
    public int Count { get; init; } = Numbers.MAX_LIMIT_QUERY;

    /// <summary>
    ///     动态查询条件
    /// </summary>
    public DynamicFilterInfo DynamicFilter { get; init; }

    /// <summary>
    ///     查询条件
    /// </summary>
    public T Filter { get; init; }

    /// <summary>
    ///     忽略归属
    /// </summary>
    [JsonIgnore]
    public bool IgnoreOwner { get; init; }

    /// <summary>
    ///     查询关键字
    /// </summary>
    public string Keywords { get; init; }

    /// <summary>
    ///     排序方式
    /// </summary>
    public Orders? Order { get; init; }

    /// <summary>
    ///     排序字段
    /// </summary>
    public string Prop { get; init; }

    /// <summary>
    ///     所需字段
    /// </summary>
    public string[] RequiredFields { get; init; }

    /// <summary>
    ///     列表表达式
    /// </summary>
    public Expression<Func<TEntity, TEntity>> GetToListExp<TEntity>()
    {
        if (RequiredFields.NullOrEmpty()) {
            return null;
        }

        var expParameter = Expression.Parameter(typeof(TEntity), "a");
        var bindings     = new List<(PropertyInfo, MemberInitExpression)>();

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var field in RequiredFields) {
            var prop = typeof(TEntity).GetRecursiveProperty(field);
            if (prop == null || prop.GetCustomAttribute<DangerFieldAttribute>() != null) {
                continue;
            }

            var parentPath     = field[..field.LastIndexOf('.').Is(-1, field.Length)];
            var parentProperty = typeof(TEntity).GetRecursiveProperty(parentPath);
            var propExp        = Expression.Property(Expression.Parameter(prop.DeclaringType!, parentPath), prop);

            bindings.Add((parentProperty, Expression.MemberInit(Expression.New(prop.DeclaringType), Expression.Bind(prop, propExp))));
        }

        var expBody = Expression.MemberInit( //
            Expression.New(typeof(TEntity))
          , bindings.SelectMany(x => x.Item1.PropertyType == x.Item2.Type ? [Expression.Bind(x.Item1, x.Item2)] : x.Item2.Bindings.ToArray()));
        return Expression.Lambda<Func<TEntity, TEntity>>(expBody, expParameter);
    }
}