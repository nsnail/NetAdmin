using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String 扩展方法
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     计算Crc32
    /// </summary>
    public static int Crc32(this string me)
    {
        return BitConverter.ToInt32(System.IO.Hashing.Crc32.Hash(Encoding.UTF8.GetBytes(me)));
    }

    /// <summary>
    ///     执行C#代码
    /// </summary>
    public static Task<T> ExecuteCSharpCodeAsync<T>(this   string   me, Assembly[] assemblies
                                                  , params string[] importNamespaces)
    {
        // 使用 Roslyn 编译并执行代码
        return CSharpScript.EvaluateAsync<T>(
            me, ScriptOptions.Default.WithReferences(assemblies).WithImports(importNamespaces));
    }

    /// <summary>
    ///     object -> json
    /// </summary>
    public static T ToObject<T>(this string me)
    {
        return me.Object<T>(GlobalStatic.JsonSerializerOptions);
    }

    /// <summary>
    ///     object -> json
    /// </summary>
    public static object ToObject(this string me, Type toType)
    {
        return me.Object(toType, GlobalStatic.JsonSerializerOptions);
    }

    /// <summary>
    ///     去掉尾部字符串“Async”
    /// </summary>
    #pragma warning disable RCS1047, ASA002, VSTHRD200
    public static string TrimEndAsync(this string me)
        #pragma warning restore VSTHRD200, ASA002, RCS1047
    {
        return TrimSuffix(me, "Async");
    }

    /// <summary>
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimEndOptions(this string me)
    {
        return TrimSuffix(me, "Options");
    }

    /// <summary>
    ///     去掉前部字符串
    /// </summary>
    public static string TrimPrefix(this string me, string clearStr)
    {
        return Regex.Replace(me, $"^{clearStr}", string.Empty);
    }

    /// <summary>
    ///     去掉尾部字符串
    /// </summary>
    public static string TrimSuffix(this string me, string clearStr)
    {
        return Regex.Replace(me, $"{clearStr}$", string.Empty);
    }
}