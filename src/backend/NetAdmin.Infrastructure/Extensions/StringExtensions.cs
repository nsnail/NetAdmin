using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String 扩展方法
/// </summary>
public static class StringExtensions
{
    private const           string _CHARACTERS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static readonly Regex  _regexIpV4  = new(Chars.RGXL_IP_V4);

    /// <summary>
    ///     将指定的输入字符串进行Base62解码
    /// </summary>
    /// <exception cref="ArgumentException">ArgumentException</exception>
    public static string Base62Decode(this string me)
    {
        BigInteger result = 0;

        foreach (var index in me.Select(c => _CHARACTERS.IndexOf(c))) {
            if (index < 0) {
                throw new ArgumentException("Invalid character in Base62 string.");
            }

            #pragma warning disable IDE0048, RCS1123, SA1407
            result = result * 62 + index;
            #pragma warning restore SA1407, RCS1123, IDE0048
        }

        // Convert BigInteger back to byte array and then to string
        var bytes = result.ToByteArray();

        // Handle the sign bit
        if (bytes[^1] == 0) {
            Array.Resize(ref bytes, bytes.Length - 1);
        }

        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    ///     将指定的输入字符串进行Base62编码
    /// </summary>
    public static string Base62Encode(this string me)
    {
        // Convert string to byte array
        var bytes = Encoding.UTF8.GetBytes(me);

        // Convert byte array to BigInteger for easier processing
        var bigInteger = new BigInteger(bytes);

        if (bigInteger == 0) {
            return _CHARACTERS[0].ToString();
        }

        var result = new StringBuilder();

        while (bigInteger > 0) {
            var remainder = (int)(bigInteger % 62);
            bigInteger /= 62;
            _          =  result.Insert(0, _CHARACTERS[remainder]);
        }

        return result.ToString();
    }

    /// <summary>
    ///     解码避免转义的Base64
    /// </summary>
    public static string Base64InUrlDecode(this string me)
    {
        return me.Replace("-", "+").Replace("_", "/");
    }

    /// <summary>
    ///     编码避免转义的Base64
    /// </summary>
    public static string Base64InUrlEncode(this string me)
    {
        return me.Replace("+", "-").Replace("/", "_");
    }

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
    public static Task<T> ExecuteCSharpCodeAsync<T>(this string me, Assembly[] assemblies, params string[] importNamespaces)
    {
        // 使用 Roslyn 编译并执行代码
        return CSharpScript.EvaluateAsync<T>(me, ScriptOptions.Default.WithReferences(assemblies).WithImports(importNamespaces));
    }

    /// <summary>
    ///     是否IPV4地址
    /// </summary>
    public static bool IsIpV4(this string me)
    {
        return _regexIpV4.IsMatch(me);
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

    /// <summary>
    ///     去掉尾部字符串“Async”
    /// </summary>
    #pragma warning disable RCS1047, ASA002, VSTHRD200
    public static string TrimSuffixAsync(this string me)
        #pragma warning restore VSTHRD200, ASA002, RCS1047
    {
        return TrimSuffix(me, "Async");
    }

    /// <summary>
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimSuffixOptions(this string me)
    {
        return TrimSuffix(me, "Options");
    }
}