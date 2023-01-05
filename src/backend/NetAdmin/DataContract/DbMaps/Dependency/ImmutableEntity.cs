using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Aop.Attributes;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     不可变实体
/// </summary>
public abstract record ImmutableEntity : DataAbstraction, IEntity, IFieldPrimary, IFieldAdd
{
    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_TIME)]
    [Column(CanUpdate = false, ServerTime = DateTimeKind.Local)]
    public DateTime CreatedTime { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_ID)]
    [Column(CanUpdate = false)]
    public long? CreatedUserId { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_NAME)]
    [Column(CanUpdate = false)]
    public string CreatedUserName { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_ID)]
    [Column(IsIdentity = false, IsPrimary = true)]
    [Snowflake]
    public long Id { get; set; }
}