namespace {0}.Host.WebApi.{1};

/// <summary>
///     {3}服务
/// </summary>
public class {2}Controller : ControllerBase<I{2}Service>, I{2}Module
{{
    /// <summary>
    ///     Initializes a new instance of the <see cref="{2}Controller" /> class.
    /// </summary>
    public {2}Controller(I{2}Service service) //
        : base(service) {{ }}

    /// <summary>
    ///     创建{3}
    /// </summary>
    public ValueTask<Query{2}Rsp> Create(Create{2}Req req)
    {{
        throw new NotImplementedException();
    }}

    /// <summary>
    ///     删除{3}
    /// </summary>
    public ValueTask<int> Delete(DelReq req)
    {{
        throw new NotImplementedException();
    }}

    /// <summary>
    ///     分页查询{3}
    /// </summary>
    public ValueTask<PagedQueryRsp<Query{2}Rsp>> PagedQuery(PagedQueryReq<Query{2}Req> req)
    {{
        throw new NotImplementedException();
    }}

    /// <summary>
    ///     查询{3}
    /// </summary>
    public ValueTask<List<Query{2}Rsp>> Query(QueryReq<Query{2}Req> req)
    {{
        throw new NotImplementedException();
    }}

    /// <summary>
    ///     更新{3}
    /// </summary>
    public ValueTask<Query{2}Rsp> Update(Update{2}Req req)
    {{
        throw new NotImplementedException();
    }}
}}