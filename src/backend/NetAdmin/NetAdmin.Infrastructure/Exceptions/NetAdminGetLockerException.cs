#pragma warning disable RCS1194
namespace NetAdmin.Infrastructure.Exceptions;

/// <summary>
///     加锁失败异常
/// </summary>
/// <remarks>
///     并发执行时锁竞争失败
/// </remarks>
public sealed class NetAdminGetLockerException(string message = null) : NetAdminInvalidOperationException(message);