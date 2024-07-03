using CsvHelper;
using Microsoft.Net.Http.Headers;
using NetAdmin.Application.Repositories;
using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IApiService" />
public sealed class ApiService(
    BasicRepository<Sys_Api, string>    rpo                                 //
  , XmlCommentReader                    xmlCommentReader                    //
  , IActionDescriptorCollectionProvider actionDescriptorCollectionProvider) //
    : RepositoryService<Sys_Api, string, IApiService>(rpo), IApiService
{
    /// <inheritdoc />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .CountAsync();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<int> DeleteAsync(DelReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        return QueryInternal(req)
            #if DBTYPE_SQLSERVER
               .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
            #endif
            .AnyAsync();
    }

    /// <inheritdoc />
    public async Task<IActionResult> ExportAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        var data = await QueryInternal(req)
                         #if DBTYPE_SQLSERVER
                         .WithLock(SqlServerLock.NoLock | SqlServerLock.NoWait)
                         #endif
                         .Take(Numbers.MAX_LIMIT_EXPORT)
                         .ToListAsync()
                         .ConfigureAwait(false);
        var list   = data.Adapt<List<ExportApiRsp>>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        var csv    = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteHeader<ExportApiRsp>();
        await csv.NextRecordAsync().ConfigureAwait(false);

        foreach (var item in list) {
            csv.WriteRecord(item);
            await csv.NextRecordAsync().ConfigureAwait(false);
        }

        await csv.FlushAsync().ConfigureAwait(false);
        _ = stream.Seek(0, SeekOrigin.Begin);

        App.HttpContext.Response.Headers.ContentDisposition
            = new ContentDispositionHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_ATTACHMENT) {
                  FileNameStar = $"{Ln.接口导出}_{DateTime.Now:yyyy.MM.dd-HH.mm.ss}.csv"
              }.ToString();
        return new FileStreamResult(stream, Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_OCTET_STREAM);
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        req.ThrowIfInvalid();
        var ret = await Rpo.Select.WhereDynamicFilter(req.DynamicFilter)
                           .WhereDynamic(req.Filter)
                           .ToTreeListAsync()
                           .ConfigureAwait(false);
        return ret.Adapt<IEnumerable<QueryApiRsp>>();
    }

    /// <inheritdoc />
    public IEnumerable<QueryApiRsp> ReflectionList(bool excludeAnonymous = true)
    {
        var regex = new Regex(@"\.(\w+)$", RegexOptions.Compiled);

        var actionDescriptors //
            = actionDescriptorCollectionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>();

        if (excludeAnonymous) {
            actionDescriptors = actionDescriptors.Where(x => x.EndpointMetadata.All(y => y is AllowAnonymousAttribute));
        }

        var actionGroup //
            = actionDescriptors.GroupBy(x => x.ControllerTypeInfo);

        return actionGroup.Select(SelectQueryApiRsp);

        QueryApiRsp SelectQueryApiRsp(IGrouping<TypeInfo, ControllerActionDescriptor> group)
        {
            var first = group.First()!;
            return new QueryApiRsp {
                                       Summary = xmlCommentReader.GetComments(group.Key)
                                     , Name    = first.ControllerName
                                     , Id = Regex.Replace( //
                                           first.AttributeRouteInfo!.Template!, $"/{first.ActionName}$", string.Empty)
                                     , Children  = GetChildren(group)
                                     , Namespace = regex.Match(group.Key.Namespace!).Groups[1].Value.ToLowerInvariant()
                                   };
        }
    }

    /// <inheritdoc />
    public async Task SyncAsync()
    {
        _ = await Rpo.DeleteAsync(_ => true).ConfigureAwait(false);

        var list = ReflectionList(false);

        EnableCascadeSave = true;
        foreach (var item in list) {
            var entity = item.Adapt<Sys_Api>();
            _ = await Rpo.InsertAsync(entity).ConfigureAwait(false);
        }
    }

    private IEnumerable<QueryApiRsp> GetChildren(IEnumerable<ControllerActionDescriptor> actionDescriptors)
    {
        return actionDescriptors //
            .Select(x => new QueryApiRsp {
                                             Summary = xmlCommentReader.GetComments(x.MethodInfo)
                                           , Name    = x.ActionName
                                           , Id      = x.AttributeRouteInfo!.Template
                                           , Method = x.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                                                       .FirstOrDefault()
                                                       ?.HttpMethods.First()
                                         });
    }

    private ISelect<Sys_Api> QueryInternal(QueryReq<QueryApiReq> req)
    {
        var ret = Rpo.Select.WhereDynamicFilter(req.DynamicFilter).WhereDynamic(req.Filter);
        switch (req.Order) {
            case Orders.None:
                return ret;
            case Orders.Random:
                return ret.OrderByRandom();
        }

        ret = ret.OrderByPropertyNameIf(req.Prop?.Length > 0, req.Prop, req.Order == Orders.Ascending);

        if (!req.Prop?.Equals(nameof(req.Filter.CreatedTime), StringComparison.OrdinalIgnoreCase) ?? true) {
            ret = ret.OrderByDescending(a => a.CreatedTime);
        }

        return ret;
    }
}