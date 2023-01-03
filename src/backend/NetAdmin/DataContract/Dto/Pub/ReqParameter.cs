namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     ReqParameter
/// </summary>
public record ReqParameter<T>
{
    /// <summary>
    ///     Req
    /// </summary>
    public T Req { get; set; }
}