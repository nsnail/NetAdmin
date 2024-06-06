using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.Dto.Dependency;

/// <inheritdoc cref="DelReq{T}" />
public sealed record DelReq : DelReq<long>;

/// <summary>
///     请求：通过编号删除
/// </summary>
public record DelReq<T> : EntityBase<T>
    where T : IEquatable<T>
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    public override T Id { get; init; }
}