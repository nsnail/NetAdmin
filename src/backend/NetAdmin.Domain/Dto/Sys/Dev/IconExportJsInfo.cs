namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     IconExportJsInfo
/// </summary>
public record IconExportJsInfo : DataAbstraction
{
    /// <summary>
    ///     ExportDefault
    /// </summary>
    public ExportDefaultRecord ExportDefault { get; init; }

    /// <summary>
    ///     ExportDefaultRecord
    /// </summary>
    public record ExportDefaultRecord
    {
        /// <summary>
        ///     Icons
        /// </summary>
        public IEnumerable<IconsItem> Icons { get; init; }
    }

    /// <summary>
    ///     IconsItem
    /// </summary>
    public record IconsItem
    {
        /// <summary>
        ///     Icons
        /// </summary>
        public ICollection<string> Icons { get; init; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; init; }
    }
}