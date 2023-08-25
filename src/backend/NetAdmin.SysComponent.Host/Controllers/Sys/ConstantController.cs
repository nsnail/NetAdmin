using NetAdmin.Domain.Dto;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     常量服务
/// </summary>
[AllowAnonymous]
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class ConstantController : ControllerBase<IConstantCache, IConstantService>, IConstantModule
{
    private readonly JsonOptions _jsonOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConstantController" /> class.
    /// </summary>
    public ConstantController(IConstantCache cache, IOptions<JsonOptions> jsonOptions) //
        : base(cache)
    {
        _jsonOptions = jsonOptions.Value;
    }

    /// <summary>
    ///     获得常量字符串
    /// </summary>
    [NonUnify]
    public IActionResult GetChars()
    {
        var ret = GetCharsDic();
        return OriginNamingResult(ret);
    }

    /// <summary>
    ///     获得常量字符串
    /// </summary>
    [NonAction]
    public IDictionary<string, string> GetCharsDic()
    {
        return Cache.GetCharsDic();
    }

    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    public IDictionary<string, Dictionary<string, string[]>> GetEnums()
    {
        return Cache.GetEnums();
    }

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    public IDictionary<string, string> GetLocalizedStrings()
    {
        return Cache.GetLocalizedStrings();
    }

    /// <summary>
    ///     获得数字常量表
    /// </summary>
    [NonUnify]
    public IActionResult GetNumbers()
    {
        var ret = GetNumbersDic();
        return OriginNamingResult(ret);
    }

    /// <summary>
    ///     获得数字常量表
    /// </summary>
    [NonAction]
    public IDictionary<string, long> GetNumbersDic()
    {
        return Cache.GetNumbersDic();
    }

    private IActionResult OriginNamingResult<T>(T data)
    {
        return new JsonResult( //
            new RestfulInfo<T> { Code                                                           = 0, Data = data }
          , new JsonSerializerOptions(_jsonOptions.JsonSerializerOptions) { DictionaryKeyPolicy = null });
    }
}