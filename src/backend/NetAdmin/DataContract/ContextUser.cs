using System.Text.Json.Serialization;
using Furion.DependencyInjection;
using NetAdmin.DataContract.DbMaps;

namespace NetAdmin.DataContract;

/// <summary>
///     上下文用户信息
/// </summary>
public record ContextUser : TbSysUser, IScoped
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override Guid Token { get; set; }
}