using System.Xml;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     程序集注释文档读取器
/// </summary>
public sealed class XmlCommentReader : ISingleton
{
    private const           string            _XPATH        = "//doc/members/member[@name=\"{0}\"]";
    private static readonly Regex             _regex        = new(@"`\d+");
    private readonly        List<XmlDocument> _xmlDocuments = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="XmlCommentReader" /> class.
    /// </summary>
    public XmlCommentReader(IOptions<SpecificationDocumentSettingsOptions> specificationDocumentSettings)
    {
        var xmlComments = specificationDocumentSettings.Value.XmlComments //
                          ?? App.GetConfig<SpecificationDocumentSettingsOptions>(nameof(SpecificationDocumentSettingsOptions).TrimSuffixOptions())
                                .XmlComments;
        foreach (var commentFile in xmlComments.Where(x => x.Contains(nameof(NetAdmin)))) {
            var xmlDoc      = new XmlDocument();
            var xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, commentFile);
            if (!File.Exists(xmlFilePath)) {
                LogHelper.Get<XmlCommentReader>().Warn($"{Ln.XML注释文件不存在}: {xmlFilePath}");
                continue;
            }

            xmlDoc.Load(xmlFilePath);
            _xmlDocuments.Add(xmlDoc);
        }
    }

    /// <summary>
    ///     获取指定类型的注释
    /// </summary>
    /// <exception cref="InvalidCastException">InvalidCastException</exception>
    public string GetComments(MemberInfo memberInfo)
    {
        var node = memberInfo switch {
                       MethodInfo method => GetNodeByMethod(method), Type type => GetNodeByType(type), _ => throw new InvalidCastException()
                   };

        if (node?.FirstChild?.Name != "inheritdoc") {
            return node?.FirstChild?.InnerText.Trim();
        }

        var cref = node.FirstChild.Attributes?["cref"]?.Value;
        if (cref != null) {
            return GetComments(App.EffectiveTypes.Single(x => x.FullName == cref[2..]));
        }

        var methodFromBaseType = memberInfo.DeclaringType?.BaseType?.GetMethod(memberInfo.Name);

        if (methodFromBaseType != null) {
            return GetComments(methodFromBaseType);
        }

        var methodFromInterface = memberInfo.DeclaringType?.GetInterfaces().Select(x => x.GetMethod(memberInfo.Name)).FirstOrDefault(x => x != null);
        return methodFromInterface == null ? null : GetComments(methodFromInterface);
    }

    private XmlNode GetNodeByMethod(MethodInfo method)
    {
        var nodeName   = $"M:{method.DeclaringType}.{method.Name}";
        var parameters = method.GetParameters();
        if (parameters.Length != 0) {
            nodeName += $"({string.Join(',', parameters.Select(Replace))})";
        }

        return _xmlDocuments.Select(xmlDoc => xmlDoc.SelectSingleNode(
                                        #pragma warning disable CA1863
                                        string.Format(NumberFormatInfo.InvariantInfo, _XPATH, nodeName)))
                            #pragma warning restore CA1863
                            .FirstOrDefault(ret => ret != null);

        static string Replace(ParameterInfo parameterInfo)
        {
            return _regex.Replace(parameterInfo.ParameterType.ToString(), string.Empty).Replace("[", "{").Replace("]", "}");
        }
    }

    private XmlNode GetNodeByType(Type type)
    {
        return _xmlDocuments.Select(xmlDoc => xmlDoc.SelectSingleNode(
                                        #pragma warning disable CA1863
                                        string.Format(NumberFormatInfo.InvariantInfo, _XPATH, $"T:{type.FullName}")))
                            #pragma warning restore CA1863
                            .FirstOrDefault(ret => ret != null);
    }
}