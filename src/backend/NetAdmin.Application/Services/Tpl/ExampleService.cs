using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services.Tpl.Dependency;
using NetAdmin.DataContract.DbMaps.Tpl;
using NetAdmin.DataContract.Dto.Tpl;

namespace NetAdmin.Application.Services.Tpl;

/// <inheritdoc cref="NetAdmin.Application.Services.Tpl.Dependency.IExampleService" />
public class ExampleService : RepositoryService<TbTplExample, IExampleService>, IExampleService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExampleService" /> class.
    /// </summary>
    public ExampleService(Repository<TbTplExample> rpo) //
        : base(rpo) { }

    /// <inheritdoc />
    public ValueTask<QueryExampleRsp> Create(CreateExampleReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ValueTask<int> Delete(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ValueTask<PagedQueryRsp<QueryExampleRsp>> PagedQuery(PagedQueryReq<QueryExampleReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ValueTask<List<QueryExampleRsp>> Query(QueryReq<QueryExampleReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public ValueTask<QueryExampleRsp> Update(UpdateExampleReq req)
    {
        throw new NotImplementedException();
    }
}