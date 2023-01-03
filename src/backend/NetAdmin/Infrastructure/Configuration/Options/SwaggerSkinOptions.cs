using NetAdmin.Infrastructure.Configuration.Options.SubNodes.SwaggerSkin;

namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     API 界面 knife4j-vue 配置
/// </summary>
public record SwaggerSkinOptions : OptionAbstraction
{
    /// <summary>
    ///     Urls
    /// </summary>
    public IEnumerable<UrlDescriptorNode> Urls { get; set; }
}