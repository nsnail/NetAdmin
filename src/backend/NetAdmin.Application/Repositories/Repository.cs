using System.Linq.Expressions;
using FreeSql;
using FreeSql.Internal.Model;
using NetAdmin.DataContract;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.Application.Repositories;

/// <inheritdoc cref="IRepository{TEntity}" />
public class Repository<TEntity> : DefaultRepository<TEntity, long>, IRepository<TEntity>
    where TEntity : EntityBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
    /// </summary>
    public Repository(IFreeSql fsql, UnitOfWorkManager uowManger) //
        : base(fsql, uowManger) { }

    /// <inheritdoc />
    public ContextUser ContextUser { get; init; }

    /// <inheritdoc />
    public virtual async Task<bool> DeleteRecursiveAsync( //
        Expression<Func<TEntity, bool>> exp, params string[] disableGlobalFilterNames)
    {
        await Select.Where(exp)
                    .DisableGlobalFilter(disableGlobalFilterNames)
                    .AsTreeCte()
                    .ToDelete()
                    .ExecuteAffrowsAsync();

        return true;
    }

    /// <inheritdoc />
    public virtual Task<TDto> GetAsync<TDto>(long id)
    {
        return Select.WhereDynamic(id).ToOneAsync<TDto>();
    }

    /// <inheritdoc />
    public virtual Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> exp)
    {
        return Select.Where(exp).ToOneAsync<TDto>();
    }

    /// <inheritdoc />
    public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
    {
        return Select.Where(exp).ToOneAsync();
    }

    /// <summary>
    ///     获取分页列表
    /// </summary>
    /// <param name="dynamicFilterInfo">动态过滤器</param>
    /// <param name="page">页码</param>
    /// <param name="pageSize">页容量</param>
    /// <returns>分页列表和总条数</returns>
    public virtual async Task<(List<TEntity> List, long Total)> GetPagedListAsync(
        DynamicFilterInfo dynamicFilterInfo, int page, int pageSize)
    {
        var list = await Select.WhereDynamicFilter(dynamicFilterInfo)
                               .Count(out var total)
                               .Page(page, pageSize)
                               .ToListAsync();
        return (list, total);
    }
}