using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NSExt.Extensions;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     常量服务
/// </summary>
[AllowAnonymous]
public class ConstantController : ControllerBase<IConstantService>, IConstantModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ConstantController" /> class.
    /// </summary>
    public ConstantController(IConstantService service) //
        : base(service) { }

    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    public object GetEnums()
    {
        return Service.GetEnums();
    }

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    public object GetLocalizedStrings()
    {
        return Service.GetLocalizedStrings();
    }

    /// <summary>
    ///     获得常量字符串
    /// </summary>
    [NonUnify]
    public dynamic GetStrings()
    {
        var ret = Service.GetStrings();
        return OriginNamingResult(ret);
    }

    private static IActionResult OriginNamingResult(ImmutableSortedDictionary<string, string> data)
    {
        var jsonOptions = default(JsonSerializerOptions).NewJsonSerializerOptions();
        jsonOptions.DictionaryKeyPolicy = null;
        return new JsonResult(new RestfulInfo<object> { Code = 0, Data = data }, jsonOptions);
    }
}