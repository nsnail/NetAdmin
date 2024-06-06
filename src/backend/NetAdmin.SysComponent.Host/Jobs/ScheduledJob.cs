using FreeSql.Internal;
using Furion.RemoteRequest;
using Furion.RemoteRequest.Extensions;
using Furion.Schedule;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.Host.BackgroundRunning;
using NetAdmin.Host.Extensions;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Jobs;

/// <summary>
///     计划作业
/// </summary>
public sealed class ScheduledJob : WorkBase<ScheduledJob>, IJob
{
    private static   string                _accessToken;
    private static   string                _refreshToken;
    private readonly IJobRecordService     _jobRecordService;
    private readonly IJobService           _jobService;
    private readonly ILogger<ScheduledJob> _logger;
    private readonly IUserService          _userService;
    private          string                _requestHeader;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ScheduledJob" /> class.
    /// </summary>
    public ScheduledJob()
    {
        _jobRecordService = ServiceProvider.GetService<IJobRecordService>();
        _jobService       = ServiceProvider.GetService<IJobService>();
        _logger           = ServiceProvider.GetService<ILogger<ScheduledJob>>();
        _userService      = ServiceProvider.GetService<IUserService>();
    }

    /// <summary>
    ///     具体处理逻辑
    /// </summary>
    /// <param name="context">作业执行前上下文</param>
    /// <param name="stoppingToken">取消任务 Token</param>
    /// <exception cref="NetAdminGetLockerException">加锁失败异常</exception>
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        await WorkflowAsync(stoppingToken).ConfigureAwait(false);
    }

    /// <summary>
    ///     通用工作流
    /// </summary>
    /// <exception cref="NotImplementedException">NotImplementedException</exception>
    /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
    protected override async ValueTask WorkflowAsync(CancellationToken cancelToken)
    {
        QueryJobRsp job = null;
        try {
            job = await _jobService.GetNextJobAsync().ConfigureAwait(false);
        }
        catch (DbUpdateVersionException) {
            // ignore
        }

        if (job == null) {
            _logger.Info(Ln.未获取到待执行任务);
            return;
        }

        var request = BuildRequest(job);
        var sw      = new Stopwatch();
        sw.Start();
        var rsp = await request.SendAsync(CancellationToken.None).ConfigureAwait(false);
        if (rsp.StatusCode == HttpStatusCode.Unauthorized) {
            var loginRsp = await _userService.LoginByUserIdAsync(job.UserId).ConfigureAwait(false);
            #pragma warning disable S2696
            _accessToken  = loginRsp.AccessToken;
            _refreshToken = loginRsp.RefreshToken;
            #pragma warning restore S2696
            request = BuildRequest(job);
            rsp     = await request.SendAsync(CancellationToken.None).ConfigureAwait(false);
        }

        sw.Stop();
        await UowManager.AtomicOperateAsync(async () => {
                            var rspBody = await rsp.Content.ReadAsStringAsync(CancellationToken.None)
                                                   .ConfigureAwait(false);
                            var jobRecord = new CreateJobRecordReq //
                                            {
                                                Duration       = sw.ElapsedMilliseconds
                                              , HttpMethod     = job.HttpMethod
                                              , HttpStatusCode = (int)rsp.StatusCode
                                              , JobId          = job.Id
                                              , RequestBody    = job.RequestBody
                                              , RequestHeader  = _requestHeader
                                              , RequestUrl     = job.RequestUrl
                                              , ResponseBody   = rspBody
                                              , ResponseHeader = rsp.Headers.Json()
                                              , TimeId         = job.NextTimeId!.Value
                                            };
                            _ = await _jobRecordService.CreateAsync(jobRecord).ConfigureAwait(false);
                            await _jobService
                                  .FinishJobAsync(job.Adapt<FinishJobReq>() with {
                                                      LastStatusCode = rsp.StatusCode
                                                    , LastDuration = jobRecord.Duration
                                                  })
                                  .ConfigureAwait(false);
                        })
                        .ConfigureAwait(false);
    }

    private HttpRequestPart BuildRequest(QueryJobRsp job)
    {
        var ret     = job.RequestUrl.SetHttpMethod(new HttpMethod(job.HttpMethod.ToString()));
        var headers = new Dictionary<string, string>();

        if (!_accessToken.NullOrEmpty()) {
            headers.Add( //
                Chars.FLG_HTTP_HEADER_KEY_AUTHORIZATION, $"{Chars.FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA} {_accessToken}");
        }

        if (!_refreshToken.NullOrEmpty()) {
            headers.Add( //
                Chars.FLG_HTTP_HEADER_KEY_X_ACCESS_TOKEN_HEADER_KEY
              , $"{Chars.FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA} {_refreshToken}");
        }

        if (!job.RequestHeader.NullOrEmpty()) {
            ret = ret.SetHeaders(headers.Union(job.RequestHeader.Object<Dictionary<string, string>>())
                                        .ToDictionary(x => x.Key, x => x.Value));
        }

        if (!job.RequestBody.NullOrEmpty()) {
            ret = ret.SetBody(job.RequestBody);
        }

        return ret.SetLog(_logger).OnResponsing(GetRequestHeader).OnException(GetRequestHeader);
    }

    private void GetRequestHeader(HttpClient _, HttpResponseMessage rsp, string __)
    {
        _requestHeader = rsp!.RequestMessage!.Headers.Json();
    }

    private void GetRequestHeader(HttpClient _, HttpResponseMessage rsp)
    {
        GetRequestHeader(_, rsp, null);
    }
}