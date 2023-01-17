using System.ComponentModel;
using NSExt.Attributes;

namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     数据库实体基类
/// </summary>
public abstract record EntityBase : DataAbstraction
{
    /// <summary>
    ///     公用设置
    /// </summary>
    public enum BitSets : long
    {
        /// <summary>
        ///     启用
        /// </summary>
        [Description(nameof(Ln.Enabled))]
        [Localization(typeof(Ln))]
        Enabled = 0b_0001

       ,

        /// <summary>
        ///     暂未定义
        /// </summary>
        [Description(nameof(Ln.Undefined))]
        [Localization(typeof(Ln))]
        Undefined1 = 0b_0010

       ,

        /// <summary>
        ///     暂未定义
        /// </summary>
        [Description(nameof(Ln.Undefined))]
        [Localization(typeof(Ln))]
        Undefined2 = 0b_0100

       ,

        /// <summary>
        ///     暂未定义
        /// </summary>
        [Description(nameof(Ln.Undefined))]
        [Localization(typeof(Ln))]
        Undefined3 = 0b_1000
    }
}