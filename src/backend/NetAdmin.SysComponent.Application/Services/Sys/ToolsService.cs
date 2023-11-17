using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Tool;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="IToolsService" />
public sealed class ToolsService : ServiceBase<IToolsService>, IToolsService
{
    /// <inheritdoc />
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        return Task.FromResult<IEnumerable<GetModulesRsp>>(AppDomain.CurrentDomain.GetAssemblies()
                                                                    .Select(x => {
                                                                        var asm = x.GetName();
                                                                        return new GetModulesRsp {
                                                                                   Name    = asm.Name
                                                                                 , Version = asm.Version?.ToString()
                                                                               };
                                                                    })
                                                                    .OrderBy(x => x.Name));
    }

    /// <inheritdoc />
    public Task<DateTime> GetServerUtcTimeAsync()
    {
        return Task.FromResult(DateTime.UtcNow);
    }

    /// <inheritdoc />
    public Task<string> GetVersionAsync()
    {
        return Task.FromResult(GlobalStatic.ProductVersion);
    }
}