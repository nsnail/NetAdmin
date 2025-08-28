namespace NetAdmin.Domain.Enums;

/// <summary>
///     HTTP 请求方法
/// </summary>
[Export]
public enum HttpMethods
{
    /// <summary>
    ///     Connect
    /// </summary>
    Connect = 1

    ,

    /// <summary>
    ///     Delete
    /// </summary>
    Delete = 2

    ,

    /// <summary>
    ///     Get
    /// </summary>
    Get = 3

    ,

    /// <summary>
    ///     Head
    /// </summary>
    Head = 4

    ,

    /// <summary>
    ///     Options
    /// </summary>
    Options = 5

    ,

    /// <summary>
    ///     Patch
    /// </summary>
    Patch = 6

    ,

    /// <summary>
    ///     Post
    /// </summary>
    Post = 7

    ,

    /// <summary>
    ///     Put
    /// </summary>
    Put = 8

    ,

    /// <summary>
    ///     Trace
    /// </summary>
    Trace = 9
}