using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Menu;

/// <summary>
///     信息：菜单
/// </summary>
public record QueryMenuRsp : TbSysMenu
{
    /// <summary>
    ///     元数据
    /// </summary>
    public MetaInfo Meta => new(Icon, Title, Type);

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public new List<QueryMenuRsp> Children { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Component { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Name { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Path { get; set; }

    /// <summary>
    ///     信息：元数据
    /// </summary>
    public record MetaInfo(string Icon, string Title, Enums.MenuTypes Type)
    {
        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; } = Icon;

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; } = Title;

        /// <summary>
        ///     类型
        /// </summary>
        public Enums.MenuTypes Type { get; set; } = Type;
    }
}