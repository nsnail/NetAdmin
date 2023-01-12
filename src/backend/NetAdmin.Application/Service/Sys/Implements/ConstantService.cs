using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.DataContract.Dto;
using NetAdmin.Infrastructure.Constant;
using NetAdmin.Infrastructure.Lang;
using NSExt.Extensions;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="IConstantService" />
[AllowAnonymous]
public class ConstantService : ServiceBase<IConstantService>, IConstantService
{
    /// <inheritdoc />
    public object GetEnums()
    {
        return typeof(Enums).GetNestedTypes(BindingFlags.Public)
                            .ToDictionary(x => x.Name, x => x.GetEnumValues()
                                                             .Cast<object>()
                                                             .ToImmutableSortedDictionary( //
                                                                 x.GetEnumName
                                                               , y => new { Value = y, Desc = ((Enum)y).Desc() }));
    }

    /// <inheritdoc />
    public object GetLocalizedStrings()
    {
        return typeof(Str).GetProperties(BindingFlags.Public | BindingFlags.Static)
                          .Where(x => x.PropertyType == typeof(string))
                          .ToImmutableSortedDictionary(x => x.Name.ToString(), x => x.GetValue(null)?.ToString());
    }

    /// <inheritdoc />
    [NonUnify]
    public IActionResult GetStrings()
    {
        var data = typeof(Strings).GetFields(BindingFlags.Public | BindingFlags.Static)
                                  .Where(x => x.FieldType == typeof(string))
                                  .ToImmutableSortedDictionary( //
                                      x => x.Name.ToString(), x => x.GetValue(null)?.ToString());

        return OriginNamingResult(data);
    }

    private static IActionResult OriginNamingResult(ImmutableSortedDictionary<string, string> data)
    {
        var jsonOptions = default(JsonSerializerOptions).NewJsonSerializerOptions();
        jsonOptions.DictionaryKeyPolicy = null;
        return new JsonResult(new RestfulInfo<object> { Code = 0, Data = data }, jsonOptions);
    }
}