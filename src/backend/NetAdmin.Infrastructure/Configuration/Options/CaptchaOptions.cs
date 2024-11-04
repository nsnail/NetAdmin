using NetAdmin.Infrastructure.Configuration.Dependency;

namespace NetAdmin.Infrastructure.Configuration.Options;

/// <summary>
///     人机验证配置
/// </summary>
public sealed record CaptchaOptions : OptionAbstraction
{
    private static readonly double _seed = BitConverter.ToInt32("yyyyMMdd"[..4].Select(x => (byte)x).ToArray());

    #pragma warning disable S3963
    static CaptchaOptions()
        #pragma warning restore S3963
    {
        var rtn = new char[32];
        for (var i = 0; i != 32; i++) {
            rtn[i] = Chars.FLGL_VISIBLE_ASCIIS[(int)(Math.Abs(Math.Sin(_seed * (i + 1))) * Chars.FLGL_VISIBLE_ASCIIS.Length)];
        }

        SecretKey = new string(rtn);
    }

    /// <summary>
    ///     密钥
    /// </summary>
    public static string SecretKey { get; }
}