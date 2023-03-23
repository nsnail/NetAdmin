namespace NetAdmin.Domain;

/// <summary>
///     数据基类
/// </summary>
public abstract record DataAbstraction
{
    /// <inheritdoc />
    public override string ToString()
    {
        return this.Json();
    }

    /// <summary>
    ///     截断所有字符串属性 以符合[MaxLength(x)]特性
    /// </summary>
    public virtual void TruncateStrings()
    {
        foreach (var property in GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(x => x.PropertyType == typeof(string))) {
            var maxLen = property.GetCustomAttribute<MaxLengthAttribute>(true)?.Length;
            if (maxLen is null or 0) {
                continue;
            }

            var value = property.GetValue(this);
            if (value is not string s || s.Length < maxLen) {
                continue;
            }

            s = s.Sub(0, maxLen.Value);
            property.SetValue(this, s);
        }
    }
}