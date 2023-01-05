using System.Text.Json;
using Furion;
using Furion.SpecificationDocument;
using NetAdmin.Aop.Attributes;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.Dto.Sys.Endpoint;
using NetAdmin.Repositories;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Api.Sys.IEndPointApi" />
public class EndPointApi : ApiCrud<TbSysEndpoint, IEndPointApi>, IEndPointApi
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EndPointApi" /> class.
    /// </summary>
    public EndPointApi(Repository<TbSysEndpoint> repository) //
        : base(repository) { }

    /// <inheritdoc />
    public async Task<HashSet<EndpointInfo>> List()
    {
        var       ret  = new HashSet<EndpointInfo>();
        using var http = new HttpClient();
        foreach (var url in SpecificationDocumentBuilder.GetOpenApiGroups()
                                                        .Select(
                                                            x =>
                                                                $"{App.HttpContext.Request.Scheme}://{App.HttpContext.Request.Host}{x.RouteTemplate}")) {
            var json = await http.GetStringAsync(url);
            foreach (var p in JsonDocument.Parse(json).RootElement.GetProperty("paths").EnumerateObject()) {
                ret.Add(new EndpointInfo( //
                            p.Value.EnumerateObject().First().Value.GetProperty("summary").GetString(), p.Name));
            }
        }

        return ret;
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
}