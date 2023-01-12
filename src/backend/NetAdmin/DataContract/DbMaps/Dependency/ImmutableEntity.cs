using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Aop.Attributes;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     不可变实体
/// </summary>
public abstract record ImmutableEntity : EntityBase, IFieldPrimary, IFieldAdd
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
    public virtual long? CreatedUserId { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_NAME)]
    [Column(Position = -21, CanUpdate = false)]
    public virtual string CreatedUserName { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_ID)]
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [Snowflake]
    public virtual long Id { get; set; }
}