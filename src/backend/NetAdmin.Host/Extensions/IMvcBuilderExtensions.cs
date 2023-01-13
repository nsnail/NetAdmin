using Furion.DependencyInjection;
using NetAdmin.Host.Aop;
using NetAdmin.Infrastructure.Configuration;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     IMvcBuilder 扩展方法
/// </summary>
[SuppressSniffer]

// ReSharper disable once InconsistentNaming
public static class IMvcBuilderExtensions
{
    /// <summary>
    ///     注册Furion
    /// </summary>
    public static IMvcBuilder AddFurion(this IMvcBuilder me)
    {
        return me.AddInjectWithUnifyResult<ApiResultHandler>(injectOptions => {
            // 替换自定义的EnumSchemaFilter，支持多语言Resx资源 （需将SpecificationDocumentSettings.EnableEnumSchemaFilter配置为false)
            injectOptions.ConfigureSwaggerGen(genOptions => { genOptions.SchemaFilter<SwaggerEnumSchemaFilter>(); });
        });
    }
}