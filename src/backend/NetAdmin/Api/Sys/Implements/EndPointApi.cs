using System.Text.Json;
using Furion;
using Furion.SpecificationDocument;
using Microsoft.AspNetCore.Mvc;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Pub;
using NetAdmin.DataContract.Dto.Sys.Endpoint;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IEndPointApi" />
public class EndPointApi : RepositoryApi<TbSysEndpoint, IEndPointApi>, IEndPointApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EndPointApi" /> class.
    /// </summary>
    public EndPointApi(Repository<TbSysEndpoint> repository) //
        : base(repository) { }

    /// <inheritdoc />
    [NonAction]
    public Task Create(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> Delete(NopReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<HashSet<QueryEndpointRsp>> List()
    {
        var       ret  = new HashSet<QueryEndpointRsp>();
        using var http = new HttpClient();
        foreach (var url in SpecificationDocumentBuilder.GetOpenApiGroups()
                                                        .Select(
                                                            x =>
                                                                $"{App.HttpContext.Request.Scheme}://{App.HttpContext.Request.Host}{x.RouteTemplate}")) {
            var json = await http.GetStringAsync(url);
            foreach (var p in JsonDocument.Parse(json).RootElement.GetProperty("paths").EnumerateObject()) {
                ret.Add(new QueryEndpointRsp( //
                            p.Value.EnumerateObject().First().Value.GetProperty("summary").GetString(), p.Name));
            }
        }

        return ret;
    }

    /// <inheritdoc />
    [NonAction]
    public Task<PagedQueryRsp<QueryEndpointRsp>> PagedQuery(PagedQueryReq<NopReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [NonAction]
    public Task<List<QueryEndpointRsp>> Query(QueryReq<NopReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Transaction]
    public async Task Sync()
    {
        // 清空TbSysEndPoint表
        await Repository.DeleteAsync(x => true);

        var endpoints = await List();
        await Repository.InsertAsync(endpoints);
    }

    /// <inheritdoc />
    [NonAction]
    public Task<int> Update(NopReq req)
    {
        throw new NotImplementedException();
    }
}