using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Application.Repositories;

/// <inheritdoc cref="IRepository{TEntity}" />
public sealed class Repository<TEntity> : DefaultRepository<TEntity, long>, IRepository<TEntity>
    where TEntity : EntityBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
    /// </summary>
    public Repository(IFreeSql fSql, UnitOfWorkManager uowManger, ContextUserToken userToken) //
        : base(fSql, uowManger)
    {
        UserToken = userToken;
    }

    /// <summary>
    ///     当前上下文关联的用户
    /// </summary>
    public ContextUserToken UserToken { get; }

    /// <summary>
    ///     递归删除
    /// </summary>
    /// <param name="whereExp">条件表达式</param>
    /// <param name="disableGlobalFilterNames">禁用全局过滤器名</param>
    public async Task<bool> DeleteRecursiveAsync( //
        Expression<Func<TEntity, bool>> whereExp, params string[] disableGlobalFilterNames)
    {
        _ = await Select.Where(whereExp)
                        .DisableGlobalFilter(disableGlobalFilterNames)
                        .AsTreeCte()
                        .ToDelete()
                        .ExecuteAffrowsAsync();

        return true;
    }

    /// <summary>
    ///     获得Dto
    /// </summary>
    /// <param name="id">主键</param>
    public Task<TDto> GetAsync<TDto>(long id)
    {
        return Select.WhereDynamic(id).ToOneAsync<TDto>();
    }

    /// <summary>
    ///     根据条件获取Dto
    /// </summary>
    /// <param name="whereExp">条件表达式</param>
    public Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> whereExp)
    {
        return Select.Where(whereExp).ToOneAsync<TDto>();
    }

    /// <summary>
    ///     根据条件获取实体
    /// </summary>
    /// <param name="whereExp">条件表达式</param>
    public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExp)
    {
        return Select.Where(whereExp).ToOneAsync();
    }

    /// <summary>
    ///     获取分页列表
    /// </summary>
    /// <param name="dynamicFilterInfo">动态过滤器</param>
    /// <param name="page">页码</param>
    /// <param name="pageSize">页容量</param>
    /// <returns>（分页列表，总条数）</returns>
    public async Task<(IEnumerable<TEntity> List, long Total)> GetPagedListAsync(
        DynamicFilterInfo dynamicFilterInfo, int page, int pageSize)
    {
        var list = await Select.WhereDynamicFilter(dynamicFilterInfo)
                               .Count(out var total)
                               .Page(page, pageSize)
                               .ToListAsync();
        return (list, total);
    }
}