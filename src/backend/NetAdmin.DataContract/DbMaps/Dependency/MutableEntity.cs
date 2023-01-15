using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     可变实体
/// </summary>
public abstract record MutableEntity : ImmutableEntity, IFieldUpdate
{
    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_MODIFIED_TIME)]
    [Column(Position = -11, CanInsert = false, ServerTime = DateTimeKind.Local)]
    public virtual DateTime? ModifiedTime { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_MODIFIED_USER_ID)]
    [Column(Position = -13, CanInsert = false)]
    public virtual long? ModifiedUserId { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_MODIFIED_USER_NAME)]
    [Column(Position = -12, CanInsert = false)]
    public virtual string ModifiedUserName { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_VERSION)]
    [Column(Position = -10, IsVersion = true)]
    public virtual long Version { get; init; }
}