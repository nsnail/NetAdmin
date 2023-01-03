using System.ComponentModel;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Attributes;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     默认实体
/// </summary>
public abstract record DefaultEntity : DataAbstraction, IEntity, IFieldPrimary, IFieldAdd, IFieldUpdate, IFieldDelete
                                     , IFieldVersion
{
    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_TIME)]
    [Column(CanUpdate = false, ServerTime = DateTimeKind.Local)]
    public virtual DateTime CreatedTime { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_ID)]
    [Column(CanUpdate = false)]
    public virtual long? CreatedUserId { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_CREATED_USER_NAME)]
    [Column(CanUpdate = false)]
    public virtual string CreatedUserName { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_ID)]
    [Column(IsIdentity = false, IsPrimary = true)]
    [Snowflake]
    public virtual long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_IS_DELETED)]
    public virtual bool IsDeleted { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DESC_MODIFIED_TIME)]
    [Column(CanInsert = false, ServerTime = DateTimeKind.Local)]
    public virtual DateTime? ModifiedTime { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_MODIFIED_USER_ID)]
    [Column(CanInsert = false)]
    public virtual long? ModifiedUserId { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_MODIFIED_USER_NAME)]
    [Column(CanInsert = false)]
    public virtual string ModifiedUserName { get; set; }

    /// <inheritdoc />
    [JsonIgnore]
    [Description(Strings.DSC_VERSION)]
    [Column(IsVersion = true)]
    public virtual long Version { get; set; }
}