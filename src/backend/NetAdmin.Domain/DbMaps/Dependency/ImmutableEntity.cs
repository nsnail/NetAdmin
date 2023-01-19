using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.Attributes;
using NSExt.Attributes;

namespace NetAdmin.Domain.DbMaps.Dependency;

/// <summary>
///     不可变实体
/// </summary>
public abstract record ImmutableEntity : ImmutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    public override long Id { get; init; }
}

/// <summary>
///     不可变实体
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record ImmutableEntity<T> : EntityBase, IFieldPrimary<T>, IFieldAdd
{
    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Ln.Created_time))]
    [Localization(typeof(Ln))]
    [Column(Position = -20, CanUpdate = false, ServerTime = DateTimeKind.Local)]
    public virtual DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Ln.Creator_id))]
    [Localization(typeof(Ln))]
    [Column(Position = -22, CanUpdate = false)]
    public virtual long? CreatedUserId { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Ln.Creator_username))]
    [Localization(typeof(Ln))]
    [Column(Position = -21, CanUpdate = false)]
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    [JsonIgnore]
    [Description(nameof(Ln.Unique_id))]
    [Localization(typeof(Ln))]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public virtual T Id { get; init; }
}