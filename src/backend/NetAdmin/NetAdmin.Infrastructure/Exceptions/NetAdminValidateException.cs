#pragma warning disable RCS1194
namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     验证失败异常
/// </summary>
/// <remarks>
///     手动调用模型验证方法抛出
/// </remarks>
public sealed class NetAdminValidateException(Dictionary<string, string[]> validateResults) : NetAdminInvalidInputException
{
    /// <summary>
    ///     验证结果
    /// </summary>
    public Dictionary<string, string[]> ValidateResults { get; } = validateResults;
}