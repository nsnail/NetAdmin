namespace NetAdmin.SysComponent.Domain.Dto.Sys.Menu;

/// <summary>
///     请求：查询菜单
/// </summary>
public sealed record QueryMenuReq : Sys_Menu
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}