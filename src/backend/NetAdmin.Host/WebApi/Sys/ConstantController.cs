using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     常量服务
/// </summary>
[AllowAnonymous]
public class ConstantController : ControllerBase<IConstantService>, IConstantModule
{
    private readonly JsonOptions _jsonOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConstantController" /> class.
    /// </summary>
    public ConstantController(IConstantService service, IOptions<JsonOptions> jsonOptions) //
        : base(service)
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
        var ret = Service.GetCharsDic();
        return ret;
    }

    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    public IDictionary<string, Dictionary<string, string>> GetEnums()
    {
        return Service.GetEnums();
    }

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    public IDictionary<string, string> GetLocalizedStrings()
    {
        return Service.GetLocalizedStrings();
    }

    private IActionResult OriginNamingResult(IDictionary<string, string> data)
    {
        return new JsonResult( //
            new RestfulInfo<IDictionary<string, string>> { Code = Enums.RspCodes.Succeed, Data = data }
          , new JsonSerializerOptions(_jsonOptions.JsonSerializerOptions) { DictionaryKeyPolicy = null });
    }
}