using {0}.Application.Repositories;
using {0}.Application.Services.{1}.Dependency;

namespace {0}.Application.Services.{1};

/// <inheritdoc cref="I{2}Service" />
public class {2}Service : RepositoryService<Tb{1}{2}, I{2}Service>, I{2}Service
{{
    /// <summary>
    ///     Initializes a new instance of the <see cref="{2}Service" /> class.
    /// </summary>
    public {2}Service(Repository<Tb{1}{2}> rpo) //
        : base(rpo) {{ }}

    /// <inheritdoc />
    public ValueTask<Query{2}Rsp> Create(Create{2}Req req)
    {{
        throw new NotImplementedException();
    }}

    /// <inheritdoc />
    public ValueTask<int> Delete(DelReq req)
    {{
        throw new NotImplementedException();
    }}

    /// <inheritdoc />
    public ValueTask<PagedQueryRsp<Query{2}Rsp>> PagedQuery(PagedQueryReq<Query{2}Req> req)
    {{
        throw new NotImplementedException();
    }}

    /// <inheritdoc />
    public ValueTask<List<Query{2}Rsp>> Query(QueryReq<Query{2}Req> req)
    {{
       var ret = await QueryInternal(req).Take(Numbers.QUERY_LIMIT).ToListAsync();
            return ret.ConvertAll(x => x.Adapt<Query{2}Rsp>());
    }}


    /// <inheritdoc />
    public async ValueTask<PagedQueryRsp<Query{2}Rsp>> PagedQuery(PagedQueryReq<Query{2}Req> req)
    {
        var list = await QueryInternal(req).Page(req.Page, req.PageSize).Count(out var total).ToListAsync();

        return new PagedQueryRsp<Query{2}Rsp>(req.Page, req.PageSize, total
                                                 , list.Select(x => x.Adapt<Query{2}Rsp>()));
    }

    /// <inheritdoc />
    public ValueTask<Query{2}Rsp> Update(Update{2}Req req)
    {{
        throw new NotImplementedException();
    }}

    private ISelect<Tb{1}}{2}> QueryInternal(QueryReq<Query{2}Req> req)
    {{
         var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                      .WhereDynamic(req.Filter)
                      .OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Enums.Orders.Ascending)
                      .OrderByDescending(a => a.Id);
         return ret;
    }}
}}