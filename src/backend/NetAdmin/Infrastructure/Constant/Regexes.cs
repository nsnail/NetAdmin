using System.Text.RegularExpressions;

namespace NetAdmin.Infrastructure.Constant;

// 注意：使用 [GeneratedRegex] 新特性会生成重复key值的xmlcomment导致出错
internal static class Regexes
{
    public static readonly Regex
        RegexDigitDot3 = new(@"([0-9\.]{3,})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexEndWithApi = new("Api$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexEndWithOptions = new("Options$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex
        RegexIndexHtml = new("^/?index.html$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexSlash = new("^/*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexSqlFrom = new(@"(From ""\w+"")", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexSwaggerResources
        = new("^/swagger-resources$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
}