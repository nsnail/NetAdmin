using System.Text.RegularExpressions;

namespace NetAdmin.Infrastructure.Constant;

// 注意：使用 [GeneratedRegex] 新特性会生成重复key值的xmlcomment导致出错
internal static class Regexes
{
    public static readonly Regex RegexEndWithApi = new("Api$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly Regex RegexEndWithOptions = new("Options$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
}