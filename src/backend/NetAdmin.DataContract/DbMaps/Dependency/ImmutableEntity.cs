using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.Attributes;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     不可变实体
/// </summary>
public abstract record ImmutableEntity : ImmutableEntity<long>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    [Snowflake]
    public override long Id { get; set; }
}

/// <summary>
///     不可变实体
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record ImmutableEntity<T> : EntityBase, IFieldPrimary<T>, IFieldAdd
{
    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_TIME)]
    [Column(Position = -20, CanUpdate = false, ServerTime = DateTimeKind.Local)]
    public virtual DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_ID)]
    [Column(Position = -22, CanUpdate = false)]
    public virtual long? CreatedUserId { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_NAME)]
    [Column(Position = -21, CanUpdate = false)]
    public virtual string CreatedUserName { get; set; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    [JsonIgnore]
    [Description(Strings.DSC_ID)]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    public virtual T Id { get; set; }
}