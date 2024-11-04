namespace NetAdmin.SysComponent.Domain.Dto.Sys.Dev;

/// <summary>
///     IconExportJsInfo
/// </summary>
public sealed record IconExportJsInfo : DataAbstraction
{
    /// <summary>
    ///     ExportDefault
    /// </summary>
    public ExportDefaultRecord ExportDefault { get; init; }

    /// <summary>
    ///     ExportDefaultRecord
    /// </summary>
    public sealed record ExportDefaultRecord
    {
        /// <summary>
        ///     Icons
        /// </summary>
        public IEnumerable<IconsItem> Icons { get; init; }
    }

    /// <summary>
    ///     IconsItem
    /// </summary>
    public sealed record IconsItem
    {
        /// <summary>
        ///     Icons
        /// </summary>
        [JsonInclude]
        public ICollection<string> Icons { get; init; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; init; }
    }
}