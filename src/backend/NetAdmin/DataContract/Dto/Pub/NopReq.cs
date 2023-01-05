using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Pub;

/// <inheritdoc />
public record NopReq : DataAbstraction
{
    /// <summary>
    ///     Oper
    /// </summary>
    public Enums.Operators Operators { get; set; }
}