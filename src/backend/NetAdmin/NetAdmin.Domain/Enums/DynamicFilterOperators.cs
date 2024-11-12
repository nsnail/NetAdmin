namespace NetAdmin.Domain.Enums;

/// <summary>
///     动态查询条件操作符
/// </summary>
[Export]
public enum DynamicFilterOperators
{
    /// <summary>
    ///     包含
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.包含))]
    Contains = 0

   ,

    /// <summary>
    ///     以什么开始
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.以什么开始))]
    StartsWith = 1

   ,

    /// <summary>
    ///     以什么结束
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.以什么结束))]
    EndsWith = 2

   ,

    /// <summary>
    ///     不包含
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.不包含))]
    NotContains = 3

   ,

    /// <summary>
    ///     不以什么开始
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.不以什么开始))]
    NotStartsWith = 4

   ,

    /// <summary>
    ///     不以什么结束
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.不以什么结束))]
    NotEndsWith = 5

   ,

    /// <summary>
    ///     等于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.等于))]
    Equal = 6

   ,

    /// <summary>
    ///     等于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.等于))]
    Equals = 7

   ,

    /// <summary>
    ///     等于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.等于))]
    Eq = 8

   ,

    /// <summary>
    ///     不等于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.不等于))]
    NotEqual = 9

   ,

    /// <summary>
    ///     大于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.大于))]
    GreaterThan = 10

   ,

    /// <summary>
    ///     大于等于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.大于等于))]
    GreaterThanOrEqual = 11

   ,

    /// <summary>
    ///     小于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.小于))]
    LessThan = 12

   ,

    /// <summary>
    ///     小于等于
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.小于等于))]
    LessThanOrEqual = 13

   ,

    /// <summary>
    ///     范围
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.范围))]
    Range = 14

   ,

    /// <summary>
    ///     日期范围
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.日期范围))]
    DateRange = 15

   ,

    /// <summary>
    ///     为其中之一
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.为其中之一))]
    Any = 16

   ,

    /// <summary>
    ///     不为其中之一
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.不为其中之一))]
    NotAny = 17

   ,

    /// <summary>
    ///     自定义
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.自定义))]
    Custom = 18
}