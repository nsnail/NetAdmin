using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Furion.DependencyInjection;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     程序集注释文档读取工具
/// </summary>
public class XmlCommentHelper : ISingleton
{
    private const    string      _XPATH       = "//doc/members/member[@name=\"{0}\"]";
    private readonly XmlDocument _xmlDocument = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="XmlCommentHelper" /> class.
    /// </summary>
    public XmlCommentHelper()
    {
        _xmlDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{nameof(NetAdmin)}.xml"));
    }

    /// <summary>
    ///     获取指定类型的注释
    /// </summary>
    public string GetComments(MemberInfo memberInfo)
    {
        var node = memberInfo switch {
                       MethodInfo method => GetNodeByMethod(method)
                     , Type type         => GetNodeByType(type)
                     , _                 => throw new InvalidCastException()
                   };

        if (node?.FirstChild?.Name != "inheritdoc") {
            return node?.FirstChild?.InnerText.Trim();
        }

        var cref = node.FirstChild.Attributes?["cref"]?.Value;
        if (cref is not null) {
            return GetComments(Type.GetType(cref[2..]));
        }

        var methodFromBaseType = memberInfo.DeclaringType?.BaseType?.GetMethod(memberInfo.Name);

        if (methodFromBaseType is not null) {
            return GetComments(methodFromBaseType);
        }

        var methodFromInterface = memberInfo.DeclaringType?.GetInterfaces()
                                            .Select(x => x.GetMethod(memberInfo.Name))
                                            .FirstOrDefault(x => x is not null);
        return methodFromInterface is null ? null : GetComments(methodFromInterface);
    }

    private XmlNode GetNodeByMethod(MethodBase method)
    {
        static string Replace(ParameterInfo parameterInfo)
        {
            return Regex.Replace(parameterInfo.ParameterType.ToString(), @"`\d+\[(.+?)\]", "{$1}");
        }

        var nodeName   = $"M:{method.DeclaringType}.{method.Name}";
        var parameters = method.GetParameters();
        if (parameters.Any()) {
            nodeName += $"({string.Join(',', parameters.Select(Replace))})";
        }

        return _xmlDocument.SelectSingleNode( //
            string.Format(NumberFormatInfo.InvariantInfo, _XPATH, nodeName));
    }

    private XmlNode GetNodeByType(Type type)
    {
        return _xmlDocument.SelectSingleNode(
            string.Format(NumberFormatInfo.InvariantInfo, _XPATH, $"T:{type.FullName}"));
    }
}