namespace NetAdmin.Domain.Dto.Sys.Dev;

/// <summary>
///     IconExportJsInfo
/// </summary>
public record IconExportJsInfo : DataAbstraction
{
    /// <summary>
    ///     ExportDefault
    /// </summary>
    public ExportDefaultRecord ExportDefault { get; set; }

    /// <summary>
    ///     ExportDefaultRecord
    /// </summary>
    public record ExportDefaultRecord
    {
        /// <summary>
        ///     Icons
        /// </summary>
        public List<IconsItem> Icons { get; set; }
    }

    /// <summary>
    ///     IconsItem
    /// </summary>
    public record IconsItem
    {
        /// <summary>
        ///     Icons
        /// </summary>
        public List<string> Icons { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; }
    }
}