using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NSExt.Attributes;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     可变实体
/// </summary>
public abstract record MutableEntity : ImmutableEntity, IFieldUpdate
{
    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Str.Modification_time))]
    [Localization(typeof(Str))]
    [Column(Position = -11, CanInsert = false, ServerTime = DateTimeKind.Local)]
    public virtual DateTime? ModifiedTime { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Str.Modifier_Id))]
    [Localization(typeof(Str))]
    [Column(Position = -13, CanInsert = false)]
    public virtual long? ModifiedUserId { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Str.Modified_by))]
    [Localization(typeof(Str))]
    [Column(Position = -12, CanInsert = false)]
    public virtual string ModifiedUserName { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(nameof(Str.Version))]
    [Localization(typeof(Str))]
    [Column(Position = -10, IsVersion = true)]
    public virtual long Version { get; init; }
}