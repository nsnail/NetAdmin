namespace NetAdmin.SysComponent.Domain.Dto.Sys.Cache;

/// <summary>
///     请求：删除缓存项
/// </summary>
public sealed record DelEntryReq : DataAbstraction
{
    /// <summary>
    ///     缓存键
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.缓存键不能为空))]
    public string Key { get; init; }
}