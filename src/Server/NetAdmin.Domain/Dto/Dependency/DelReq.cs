using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Dependency;

/// <inheritdoc cref="DelReq{T}" />
public sealed record DelReq : DelReq<long>;

/// <summary>
///     请求：通过id删除
/// </summary>
/// <typeparam name="T"></typeparam>
public record DelReq<T> : DataAbstraction, IFieldPrimary<T>
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    public T Id { get; init; }
}