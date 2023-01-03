using System.Text.RegularExpressions;

namespace NetAdmin.Infrastructure.Constant;

internal sealed partial class Regexes
{
    [GeneratedRegex("Api$", RegexOptions.IgnoreCase)]
    public static partial Regex RegexEndWithApi();

    [GeneratedRegex("Options$", RegexOptions.IgnoreCase)]
    public static partial Regex RegexEndWithOptions();

    [GeneratedRegex("^/?index.html$", RegexOptions.IgnoreCase)]
    public static partial Regex RegexIndexHtml();

    [GeneratedRegex("^/*$", RegexOptions.IgnoreCase)]
    public static partial Regex RegexSlash();

    [GeneratedRegex("^/swagger-resources$", RegexOptions.IgnoreCase)]
    public static partial Regex RegexSwaggerResources();
}